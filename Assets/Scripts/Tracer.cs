using System.Collections.Generic;
using Assets.Scripts.Common.Primitives;
using Assets.Scripts.Scenes;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tracer : MonoBehaviour
    {
        [SerializeField] private ComputeShader _pathTracingShader = default;
        [SerializeField] private SceneSO _scene = default;

        private RenderTexture _target;
        private RenderTexture _converged;
        private Camera _camera;
        private uint _currentSample = 0;
        private Material _addMaterial = null;
        private List<Transform> _transformsToWatch = new List<Transform>();

        // Sphere data
        private ComputeBuffer _sphereBuffer;
        private Sphere[] _spheres;

        private void Awake()
        {
            _camera = GetComponent<Camera>();

            _transformsToWatch.Add(transform);
            _transformsToWatch.Add(_scene.DirectionalLight.transform);
        }

        private void OnEnable()
        {
            _currentSample = 0;
            CreateScene();
        }

        private void OnDisable()
        {
            if (_sphereBuffer != null)
                _sphereBuffer.Release();
        }

        private void SetShaderParameters()
        {
            // Set basic buffers
            _pathTracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
            _pathTracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
            _pathTracingShader.SetTexture(0, "_SkyboxTexture", _scene.SkyboxTexture);
            _pathTracingShader.SetVector("_PixelOffset", new Vector2(Random.value, Random.value));
            _pathTracingShader.SetFloat("_Seed", Random.value);

            // Lights
            var l = _scene.DirectionalLight.transform.forward;
            _pathTracingShader.SetVector("_DirectionalLight", new Vector4(l.x, l.y, l.z, _scene.DirectionalLight.intensity));

            if (_sphereBuffer != null)
                _pathTracingShader.SetBuffer(0, "_Spheres", _sphereBuffer);
        }

        private void CreateScene()
        {
            Random.InitState(_scene.Seed);

            // Primitives

            // Create some random spheres
            if (_spheres == null || _spheres.Length != _scene.SphereCount)
            {
                //_spheres = Spheres.GenerateRandomSphere(_sphereCount, _sphereRange, _sphereSize);
                //_spheres = Spheres.GenerateSphereArray(_sphereCount);
                _spheres = Spheres.GenerateSphereCircle(_scene.SphereCount, _scene.SphereRange, _scene.SphereSize);
            }

            // Assign to compute buffer
            if (_sphereBuffer != null)
                _sphereBuffer.Release();
            if (_spheres.Length > 0)
            {
                _sphereBuffer = new ComputeBuffer(_spheres.Length, sizeof(float) * 13);
                _sphereBuffer.SetData(_spheres);
            }

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
            _pathTracingShader.SetTexture(0, "_Result", _target);
            int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
            int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
            _pathTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

            // Blit the result texture to the screen
            if (_addMaterial == null)
                _addMaterial = new Material(Shader.Find("Hidden/AddShader"));
            _addMaterial.SetFloat("_Sample", _currentSample);
            Graphics.Blit(_target, _converged, _addMaterial);
            Graphics.Blit(_converged, destination);
            _currentSample++;
        }

        private void InitRenderTexture()
        {
            if (_target == null || _target.width != Screen.width || _target.height != Screen.height)
            {
                // Release render texture if we already have one
                if (_target != null)
                {
                    _target.Release();
                    _converged.Release();
                }

                // Get a render target for Ray Tracing
                _target = new RenderTexture(Screen.width, Screen.height, 0,
                    RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                _target.enableRandomWrite = true;
                _target.Create();
                _converged = new RenderTexture(Screen.width, Screen.height, 0,
                    RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                _converged.enableRandomWrite = true;
                _converged.Create();

                // Reset sampling
                _currentSample = 0;
            }
        }

        private void Update()
        {
            foreach (Transform t in _transformsToWatch)
            {
                if (t.hasChanged)
                {
                    _currentSample = 0;
                    t.hasChanged = false;
                }
            }
        }

    }
}
