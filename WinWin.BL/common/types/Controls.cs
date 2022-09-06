namespace WinVim.BL {
    public sealed class Controls {
        public Control[] ControlsArr { get; set; }
        public Controls(Control[] controls) {
            this.ControlsArr = controls;
        }
    }
}
