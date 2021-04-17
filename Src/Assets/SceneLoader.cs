namespace yolo {
    public interface ISceneLoader {
        Scene LoadScene();
    }

    public class ParkSceneLoader : ISceneLoader {
        public Scene LoadScene() {
            throw new System.NotImplementedException();
        }
    }
}