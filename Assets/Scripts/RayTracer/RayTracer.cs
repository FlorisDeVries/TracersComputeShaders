using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class RayTracer : MonoBehaviour
{
    public ComputeShader RayTracingShader;
    public Texture SkyboxTexture;
    private RenderTexture _target;
    private Camera _camera;
    private uint _currentSample = 0;
    private Material _addMaterial;

    // Sphere data
    ComputeBuffer sphereBuffer;
    Sphere[] spheres;
    int sphereCount = 25;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void SetShaderParameters()
    {
        RayTracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
        RayTracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
        RayTracingShader.SetTexture(0, "_SkyboxTexture", SkyboxTexture);
        RayTracingShader.SetVector("_PixelOffset", new Vector2(Random.value, Random.value));
        // RayTracingShader.SetFloats("_Spheres", new float[] { 1, 1, 1, 1, 0, 2, 0, .5f });

        CreateScene();
    }

    private void CreateScene()
    {
        if(spheres == null)
            spheres = Spheres.GenerateRandomSphere(sphereCount);

        sphereBuffer = new ComputeBuffer(spheres.Length, sizeof(float) * 7);
        sphereBuffer.SetData(spheres);
        RayTracingShader.SetBuffer(0, "spheres", sphereBuffer);
        RayTracingShader.SetFloat("sphereCount", sphereCount);
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
        RayTracingShader.SetTexture(0, "Result", _target);
        int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
        RayTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

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
        sphereBuffer.Release();
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            _currentSample = 0;
            transform.hasChanged = false;
        }
    }
}