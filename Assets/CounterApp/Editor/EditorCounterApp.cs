using UnityEditor;
using UnityEngine;

namespace FrameworkDesign.CounterApp.Editor
{
    public class EditorCounterApp : EditorWindow, FrameworkDesign.IController
    {
        [MenuItem("EditorConterApp/Open")]
        static void Open()
        {
            CounterApp.OnRegisterPatch += app =>
            {
                app.RegisterUtility<IStorage>(new EditorPrefsStorage());
            };

            var window=GetWindow<EditorCounterApp>();
            window.position = new Rect(100,100,400,600);
            window.titleContent = new GUIContent(nameof(EditorCounterApp));
            window.Show();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return CounterApp.Interface;
        }

        private void OnGUI()
        {
            if (GUILayout.Button("+"))
            {
                this.SendCommand<AddCountCommand>();
            }
            GUILayout.Label(CounterApp.Get<ICounterModel>().Count.Value.ToString());
            if (GUILayout.Button("-"))
            {
                this.SendCommand<SubCountCommand>();
            }
        }
    }
}
