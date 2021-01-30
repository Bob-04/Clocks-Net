using System.Windows;

namespace Clocks.Desktop.Tools.Controls
{
    public partial class LabelAndPasswordControl
    {
        public LabelAndPasswordControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register
        (
            "Password",
            typeof(string),
            typeof(LabelAndPasswordControl),
            new PropertyMetadata(null)
        );

        public string Password
        {
            get => (string) GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register
        (
            "Caption",
            typeof(string),
            typeof(LabelAndPasswordControl),
            new PropertyMetadata(string.Empty)
        );


        public string Caption
        {
            get => (string) GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }
    }
}
