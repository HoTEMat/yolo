using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class KeyboardManager {
        private KeyboardState? lastKbs;
        private KeyboardState? secondLastKbs;
        public void RegisterState(KeyboardState kbs) {
            secondLastKbs = lastKbs;
            lastKbs = kbs;
        }

        public bool IsKeyDown(Keys k) => lastKbs?.IsKeyDown(k) ?? false;

        public bool IsKeyPressed(Keys k) {
            if (lastKbs == null || !lastKbs.Value.IsKeyDown(k))
                return false;
            return secondLastKbs == null || !secondLastKbs.Value.IsKeyDown(k);

        }
    }
}