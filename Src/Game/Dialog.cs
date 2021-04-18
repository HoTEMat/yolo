using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yolo {

    class DialogInfo {
        List<string> sentences = new List<string>();
        public IReadOnlyList<string> Sentences => sentences;

        public DialogInfo(IEnumerable<string> sentences) {
            this.sentences = new List<string>(sentences);
        }

        public Entity OnEntity { get; init; }
        public DialogInfo NextDialog { get; init; } = null;

        public void Open() {
           // conte
        }

    }

    class DialogBehavior : Behaviour {

        public DialogBehavior(Entity e, DialogInfo info) : base(e) {

        }

        public override void Update() {



        }
    }
}
