using System;
using System.Threading.Tasks;
using Smash.Services;
using Smash.StructsAndEnums;
using TripleA.Utils.Singletons;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Smash.System
{
    public class AsyncSceneLoader : PersistentSingleton<AsyncSceneLoader>
    {
        [SerializeField] private float m_artificialDelay = 0.25f;
        [SerializeField] private Image m_fill;
        [SerializeField] private Canvas m_canvas;
        [SerializeField] private Camera m_cam;
        [SerializeField] private SceneData<MySceneTypes>[] m_scenes;
        
        private AsyncOperationHandle<SceneInstance> m_sceneLoadOperationHandle;
        private AsyncOperationHandle<SceneInstance> m_sceneUnloadOperationHandle;

        public event Action OnSceneLoadComplete;
        public event Action OnCanvasDisabled;

        protected override void Awake()
        {
            base.Awake();
            OnSceneLoadComplete += () => Debug.Log("Scene Loaded");
        }

        private async void Start()
        {
            EnableLoadingCanvas();
            int count = 0;
            bool isInitialised = false;
            do
            {
                // Debug.Log($"Initializing Authentication - {count}");
                count++;
                isInitialised = await PlayerAuthentication.Instance.InitializeUnityAuthentication();
            } while (count < 3 && !isInitialised); 
            LoadSceneByIndex(0);
        }
        
        public async void LoadSceneByIndex(int index)
        {
            await LoadScene(index: index);
        }

        private async Task LoadScene(int index)
        {
            EnableLoadingCanvas();
            
            await Task.Delay(TimeSpan.FromSeconds(m_artificialDelay));
            
            await UnloadScene();
            // Debug.Log("Loading Scene");
            
            m_sceneLoadOperationHandle = Addressables.LoadSceneAsync(m_scenes[index].sceneReference.Path, LoadSceneMode.Single, true);

            while (m_sceneLoadOperationHandle.PercentComplete < 0.9f)
            {
                m_fill.fillAmount = m_sceneLoadOperationHandle.PercentComplete;
                await Task.Delay(100);
            }
            
            m_fill.fillAmount = 1;
            await Task.Delay(TimeSpan.FromSeconds(m_artificialDelay));
            OnSceneLoadComplete?.Invoke();
            
            EnableLoadingCanvas(false);
            
            OnCanvasDisabled?.Invoke();
        }

        private async Task UnloadScene()
        {
            // Debug.Log("Unloading Scene");
            if (m_sceneLoadOperationHandle.IsValid() && SceneManager.GetActiveScene().name == m_sceneLoadOperationHandle.Result.Scene.name)
            {
                Debug.Log("Unloading Addressable Scene");
                m_sceneUnloadOperationHandle = Addressables.UnloadSceneAsync(m_sceneLoadOperationHandle);

                while (!m_sceneUnloadOperationHandle.IsDone)
                {
                    await Task.Delay(100);
                }
            }
            /*else
            {
                Debug.Log("Unloading Regular Scene");
                var operation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                if (operation != null)
                {
                    while (!operation.isDone)
                    {
                        await Task.Delay(100);
                    }
                }
            }*/
            // Debug.Log("Unloaded Scene");
        }

        private void EnableLoadingCanvas(bool enable = true)
        {
            // TODO: Animate
            m_fill.fillAmount = 0;
            m_cam.gameObject.SetActive(enable);
            m_canvas.gameObject.SetActive(enable);
        }
    }
}
