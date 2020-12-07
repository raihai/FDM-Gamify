using System;

namespace fdm_gamify2
{
    public class StateContainer
    {
        public static int Property { get; set; } = 1;

        public event Action OnChange;

        public static String SetProperty(int value)
        {
            Property = value;
            return null;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}