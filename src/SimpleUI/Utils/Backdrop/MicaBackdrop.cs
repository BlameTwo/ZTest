namespace SimpleUI.Utils.Backdrop {
    public class MicaBackdrop:WindowBackdropBase {
        public MicaBackdrop()
        {
            BackType = Type.Mica;
        }
    }

    public class AcrylicBackdrop : WindowBackdropBase {
        public AcrylicBackdrop() {
            BackType = Type.Acrylic;
        }
    }

    public class TabbedBackdrop : WindowBackdropBase {
        public TabbedBackdrop() {
            BackType = Type.Tabbed;
        }
    }
}
