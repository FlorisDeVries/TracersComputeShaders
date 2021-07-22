using System;
using Assets.Scripts.Common.Primitives;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.RayTracer
{
    public class RayTracer : MonoBehaviour
    {
        [SerializeField] private ComputeShader _rayTracingShader = default;
        [SerializeField] private Texture _skyboxTexture = default;

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
        [SerializeField] private Light _directionalLight = default;

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

            // Create some random spheres
            if (_spheres == null || _spheres.Length != _sphereCount)
            {
                //_spheres = Spheres.GenerateRandomSphere(_sphereCount, _sphereRange, _sphereSize);
                //_spheres = Spheres.GenerateSphereArray(_sphereCount);
                _spheres = Spheres.GenerateSphereCircle(_sphereCount, _sphereRange, _sphereSize);
            }

            _sphereBuffer = new ComputeBuffer(_spheres.Length, sizeof(float) * 13);
            _sphereBuffer.SetData(_spheres);
            _rayTracingShader.SetBuffer(0, "_Spheres", _sphereBuffer);

            // Lights
            Vector3 l = _directionalLight.transform.forward;
            _rayTracingShader.SetVector("_DirectionalLight", new Vector4(l.x, l.y, l.z, _directionalLight.intensity));
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            SetShaderParameters();
            Render(destination);
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

        private void OnDisable()
        {
            ReleaseBuffers();
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