using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatTaming.UI
{
    class UIManager
    {
        private static Dictionary<string, UI> screens = new Dictionary<string, UI>();
        public static UI CurrentUI => SadConsole.Global.CurrentScreen as UI;

        public static void SetUI(string UIName)
        {
            UI prevUI = CurrentUI;
            UI nextUI = screens[UIName];
            if (prevUI != null)
            {
                prevUI.Exit();
                prevUI.IsVisible = false;
                prevUI.IsVisible = false;
            }
            nextUI.IsVisible = true;
            nextUI.IsFocused = true;
            SadConsole.Global.CurrentScreen = nextUI;
            nextUI.Enter();
        }

        public static void Register(params UI[] uis)
        {
            foreach(var ui in uis)
            {
                screens[ui.Name] = ui;
            }
        }
    }
}
