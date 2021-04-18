using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yolo {

    public class DialogInfo {
        List<string> sentences = new List<string>();
        public IReadOnlyList<string> Sentences => sentences;

        public DialogInfo(IEnumerable<string> sentences) {
            this.sentences = new List<string>(sentences);
        }

        public Entity OnEntity { get; init; }
        public DialogInfo NextDialog { get; init; } = null;
        public Point Size { get; } = new(20, 3);

        public DialogBehavior OpenNewDialog(Context ctx) {
            var player = ctx.Player;
            player.FreezeFor = float.PositiveInfinity;

            var e = new Entity(ctx) {
                IsTemporal = true,
            };

            DialogBehavior result;
            e.Behavior = result = new DialogBehavior(e, this);
            e.Animation = new DialogAnimation() {
                DisplayChars = Size,
            };
            e.Position = OnEntity.Position + World.Up * 1;

            ctx.World.CurrentScene.AddEntity(e);

            return result;
        }

    }

    public class DialogBehavior : Behaviour {

        List<string> linesToDisplay;

        public DialogBehavior(Entity e, DialogInfo info) : base(e) {
            foreach (var tense in info.Sentences) {
                linesToDisplay = tense.SplitToLines(info.Size.X).ToList();
            }
        }

        public override void Update() {

            if (context.Keyboard.IsKeyPressed(Keys.F)) {
                context.Player.FreezeFor = 0;
                Entity.Destroy();
            }

        }
    }
}
