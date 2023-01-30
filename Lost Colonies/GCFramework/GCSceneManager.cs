using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gamecodeur
{
    class GCSceneManager
    {
        Dictionary<String, GCSceneBase> SceneList { get; set; }
        Dictionary<String, GCSceneBase> SceneListLoaded { get; set; }
        GCSceneBase CurrentScene;

        public GCSceneManager()
        {
            SceneList = new Dictionary<string, GCSceneBase>();
            SceneListLoaded = new Dictionary<string, GCSceneBase>();
        }

        public void AddScene(string pName, GCSceneBase pScene)
        {
            SceneList.Add(pName, pScene);
        }

        public void SetProperty(string pSceneName, string pPropertyName, string pProperty)
        {
            if (SceneList.ContainsKey(pSceneName))
            {
                // On récupère la scène dans la liste
                GCSceneBase scene = SceneList[pSceneName];
                scene.setProperty(pPropertyName, pProperty);
            }
        }

        public GCSceneBase StartScene(string pName)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Stop();
            }

            GCSceneBase scene;
            if (SceneList.ContainsKey(pName))
            {
                // On récupère la scène dans la liste
                scene = SceneList[pName];
                // Si la scène n'a pas déjà été chargée, on la charge
                if (!SceneListLoaded.ContainsKey(pName))
                {
                    // Premier chargement, on load et on start
                    scene.Load();
                    SceneListLoaded.Add(pName, scene);
                }
                scene.Focus();
                CurrentScene = scene;
                return scene;
            }
            else
            {
                Debug.Assert(false, "Scene do not exists"); ;
            }
            return null;
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public void Draw()
        {
            CurrentScene.Draw();
        }

        public void DrawGUI()
        {
            CurrentScene.DrawGUI();
        }
    }
}
