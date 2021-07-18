using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.RayTracer
{
    [ExecuteInEditMode]
    public class RayTracer : MonoBehaviour
    {
        [SerializeField] private ComputeShader _rayTracingShader;
        [SerializeField] private Texture _skyboxTexture;

        private RenderTexture _target;
        private Camera _camera;
        private uint _currentSample = 0;
        private Material _addMaterial;

        // Sphere data
        private ComputeBuffer _sphereBuffer;
        private Sphere[] _spheres;

        [Header("Primitives")]
        [Range(1, 50)]
        [SerializeField] private int _sphereCount = 25;
        [Range(5, 25)]

        [SerializeField] private float _sphereRange = 10;

        [SerializeField] private Vector2 _sphereSize = new Vector2(.1f, 2f);

        [Header("Lights")] 
        [SerializeField] private Light _directionalLight;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void SetShaderParameters()
        {
            // Set basic buffers
            _rayTracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
            _rayTracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
            _rayTracingShader.SetTexture(0, "_SkyboxTexture", _skyboxTexture);
            _rayTracingShader.SetVector("_PixelOffset", new Vector2(Random.value, Random.value));

            CreateScene();
        }

        private void CreateScene()
        {
            // Primitives
            //RandomSpheres();
            SphereArray();

            // Lights
            Vector3 l = _directionalLight.transform.forward;
            _rayTracingShader.SetVector("_DirectionalLight", new Vector4(l.x, l.y, l.z, _directionalLight.intensity));
        }

        private void RandomSpheres()
        {
            // Create some random spheres
            if (_spheres == null || _spheres.Length != _sphereCount)
                _spheres = Spheres.GenerateRandomSphere(_sphereCount, _sphereRange, _sphereSize);

            _sphereBuffer = new ComputeBuffer(_spheres.Length, sizeof(float) * 7);
            _sphereBuffer.SetData(_spheres);
            _rayTracingShader.SetBuffer(0, "_Spheres", _sphereBuffer);
            _rayTracingShader.SetFloat("_SphereCount", _sphereCount);
        }

        private void SphereArray()
        {
            // Create some random spheres
            if (_spheres == null || _spheres.Length != _sphereCount * _sphereCount)
                _spheres = Spheres.GenerateSphereArray(_sphereCount);

            _sphereBuffer = new ComputeBuffer(_spheres.Length, sizeof(float) * 7);
            _sphereBuffer.SetData(_spheres);
            _rayTracingShader.SetBuffer(0, "_Spheres", _sphereBuffer);
            _rayTracingShader.SetFloat("_SphereCount", _sphereCount * _sphereCount);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            SetShaderParameters();
            Render(destination);
            ReleaseBuffers();
        }

        private void Render(RenderTexture destination)
        {
            // Make sure we have a current render target
            InitRenderTexture();

            // Set the target and dispatch the compute shader
            _rayTracingShader.SetTexture(0, "Result", _target);
            int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
            int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
            _rayTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

            // Blit the result texture to the screen
            if (EditorApplication.isPlaying)
            {
                if (_addMaterial == null)
                    _addMaterial = new Material(Shader.Find("Hidden/AddShader"));
                _addMaterial.SetFloat("_Sample", _currentSample);
                Graphics.Blit(_target, destination, _addMaterial);
                _currentSample++;
            }
            else
            {
                Graphics.Blit(_target, destination);
            }
        }

        private void InitRenderTexture()
        {
            if (_target == null || _target.width != Screen.width || _target.height != Screen.height)
            {
                // Release render texture if we already have one
                if (_target != null)
                    _target.Release();

                // Get a render target for Ray Tracing
                _target = new RenderTexture(Screen.width, Screen.height, 0,
                    RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                _target.enableRandomWrite = true;
                _target.Create();
            }
        }

        private void ReleaseBuffers()
        {
            _sphereBuffer.Release();
        }

        private void Update()
        {
            if (transform.hasChanged || _directionalLight.transform.hasChanged)
            {
                _currentSample = 0;
                transform.hasChanged = false;
                _directionalLight.transform.hasChanged = false;
            }
        }
    }
}