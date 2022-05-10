using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Primitives;
using Assets.Scripts.Scenes;
using UnityEngine;
using Plane = Assets.Scripts.Primitives.Plane;

// ReSharper disable FieldCanBeMadeReadOnly.Local

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
        private ComputeBuffer _planeBuffer;

        private void Awake()
        {
            _camera = GetComponent<Camera>();

            _transformsToWatch.Add(transform);
            _transformsToWatch.Add(_scene.DirectionalLight.transform);
        }

        private void OnEnable()
        {
            _currentSample = 0;
            Random.InitState(_scene.Seed);
        }

        private void OnDisable()
        {
            _sphereBuffer?.Release();
            _planeBuffer?.Release();
        }

        private void SetShaderParameters()
        {
            UpdateScene();

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

            if (_planeBuffer != null)
                _pathTracingShader.SetBuffer(0, "_Planes", _planeBuffer);

        }

        private void UpdateScene()
        {
            _sphereBuffer?.Release();
            _planeBuffer?.Release();
            // Assign to compute buffer
            if (_scene.Spheres.Length > 0)
            {
                _sphereBuffer = new ComputeBuffer(_scene.SphereCount, sizeof(float) * Sphere.NumberOfFloats);
                _sphereBuffer.SetData(_scene.Spheres);
            }

            if (_scene.Planes.Length > 0)
            {
                _planeBuffer = new ComputeBuffer(_scene.PlaneCount, sizeof(float) * Plane.NumberOfFloats);
                _planeBuffer.SetData(_scene.Planes);
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
            var threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
            var threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
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
            if (_target != null && _target.width == Screen.width && _target.height == Screen.height) return;

            // Release render texture if we already have one
            if (_target != null)
            {
                _target.Release();
                _converged.Release();
            }

            // Get a render target for Ray Tracing
            _target = new RenderTexture(Screen.width, Screen.height, 0,
                    RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear)
                { enableRandomWrite = true };

            _target.Create();
            _converged = new RenderTexture(Screen.width, Screen.height, 0,
                    RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear)
                { enableRandomWrite = true };

            _converged.Create();

            // Reset sampling
            _currentSample = 0;
        }

        private void Update()
        {
            foreach (var t in _transformsToWatch.Where(t => t.hasChanged))
            {
                _currentSample = 0;
                t.hasChanged = false;
            }
        }

    }
}
