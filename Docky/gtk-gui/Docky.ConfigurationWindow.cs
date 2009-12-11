// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Docky {
    
    
    public partial class ConfigurationWindow {
        
        private Gtk.VBox vbox1;
        
        private Gtk.VBox vbox2;
        
        private Gtk.Frame frame1;
        
        private Gtk.Alignment GtkAlignment;
        
        private Gtk.VBox vbox3;
        
        private Gtk.CheckButton start_with_computer_checkbutton;
        
        private Gtk.HBox hbox4;
        
        private Gtk.Label label2;
        
        private Gtk.ComboBox theme_combo;
        
        private Gtk.Label GtkLabel2;
        
        private Gtk.Frame frame3;
        
        private Gtk.Alignment config_alignment;
        
        private Gtk.Label GtkLabel5;
        
        private Gtk.HBox hbox2;
        
        private Gtk.HBox hbox3;
        
        private Gtk.Button new_dock_button;
        
        private Gtk.Button delete_dock_button;
        
        private Gtk.VBox vbox4;
        
        private Gtk.Button close_button;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Docky.ConfigurationWindow
            this.Name = "Docky.ConfigurationWindow";
            this.Title = Mono.Unix.Catalog.GetString("Docky Configuration");
            this.Icon = Stetic.IconLoader.LoadIcon(this, "gtk-preferences", Gtk.IconSize.Menu, 16);
            this.TypeHint = ((Gdk.WindowTypeHint)(1));
            this.WindowPosition = ((Gtk.WindowPosition)(1));
            this.BorderWidth = ((uint)(2));
            // Container child Docky.ConfigurationWindow.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            // Container child vbox1.Gtk.Box+BoxChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            this.frame1 = new Gtk.Frame();
            this.frame1.Name = "frame1";
            this.frame1.ShadowType = ((Gtk.ShadowType)(2));
            // Container child frame1.Gtk.Container+ContainerChild
            this.GtkAlignment = new Gtk.Alignment(0F, 0F, 1F, 1F);
            this.GtkAlignment.Name = "GtkAlignment";
            this.GtkAlignment.LeftPadding = ((uint)(12));
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            this.vbox3 = new Gtk.VBox();
            this.vbox3.Name = "vbox3";
            this.vbox3.Spacing = 6;
            // Container child vbox3.Gtk.Box+BoxChild
            this.start_with_computer_checkbutton = new Gtk.CheckButton();
            this.start_with_computer_checkbutton.CanFocus = true;
            this.start_with_computer_checkbutton.Name = "start_with_computer_checkbutton";
            this.start_with_computer_checkbutton.Label = Mono.Unix.Catalog.GetString("Start When Computer Starts");
            this.start_with_computer_checkbutton.DrawIndicator = true;
            this.start_with_computer_checkbutton.UseUnderline = true;
            this.vbox3.Add(this.start_with_computer_checkbutton);
            Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild)(this.vbox3[this.start_with_computer_checkbutton]));
            w1.Position = 0;
            w1.Expand = false;
            w1.Fill = false;
            // Container child vbox3.Gtk.Box+BoxChild
            this.hbox4 = new Gtk.HBox();
            this.hbox4.Name = "hbox4";
            this.hbox4.Spacing = 6;
            // Container child hbox4.Gtk.Box+BoxChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.LabelProp = Mono.Unix.Catalog.GetString("Theme:");
            this.hbox4.Add(this.label2);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox4[this.label2]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child hbox4.Gtk.Box+BoxChild
            this.theme_combo = Gtk.ComboBox.NewText();
            this.theme_combo.Name = "theme_combo";
            this.hbox4.Add(this.theme_combo);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hbox4[this.theme_combo]));
            w3.Position = 1;
            w3.Expand = false;
            w3.Fill = false;
            this.vbox3.Add(this.hbox4);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox3[this.hbox4]));
            w4.Position = 1;
            w4.Expand = false;
            w4.Fill = false;
            this.GtkAlignment.Add(this.vbox3);
            this.frame1.Add(this.GtkAlignment);
            this.GtkLabel2 = new Gtk.Label();
            this.GtkLabel2.Name = "GtkLabel2";
            this.GtkLabel2.LabelProp = Mono.Unix.Catalog.GetString("<b>General Options</b>");
            this.GtkLabel2.UseMarkup = true;
            this.frame1.LabelWidget = this.GtkLabel2;
            this.vbox2.Add(this.frame1);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.vbox2[this.frame1]));
            w7.Position = 0;
            w7.Expand = false;
            w7.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.frame3 = new Gtk.Frame();
            this.frame3.Name = "frame3";
            this.frame3.ShadowType = ((Gtk.ShadowType)(2));
            // Container child frame3.Gtk.Container+ContainerChild
            this.config_alignment = new Gtk.Alignment(0F, 0F, 1F, 1F);
            this.config_alignment.Name = "config_alignment";
            this.config_alignment.LeftPadding = ((uint)(12));
            this.config_alignment.RightPadding = ((uint)(10));
            this.config_alignment.BottomPadding = ((uint)(10));
            this.frame3.Add(this.config_alignment);
            this.GtkLabel5 = new Gtk.Label();
            this.GtkLabel5.Name = "GtkLabel5";
            this.GtkLabel5.LabelProp = Mono.Unix.Catalog.GetString("<b>Dock Configuration</b>");
            this.GtkLabel5.UseMarkup = true;
            this.frame3.LabelWidget = this.GtkLabel5;
            this.vbox2.Add(this.frame3);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(this.vbox2[this.frame3]));
            w9.Position = 1;
            this.vbox1.Add(this.vbox2);
            Gtk.Box.BoxChild w10 = ((Gtk.Box.BoxChild)(this.vbox1[this.vbox2]));
            w10.Position = 0;
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 6;
            // Container child hbox2.Gtk.Box+BoxChild
            this.hbox3 = new Gtk.HBox();
            this.hbox3.Name = "hbox3";
            this.hbox3.Spacing = 6;
            // Container child hbox3.Gtk.Box+BoxChild
            this.new_dock_button = new Gtk.Button();
            this.new_dock_button.CanFocus = true;
            this.new_dock_button.Name = "new_dock_button";
            this.new_dock_button.UseUnderline = true;
            this.new_dock_button.BorderWidth = ((uint)(5));
            // Container child new_dock_button.Gtk.Container+ContainerChild
            Gtk.Alignment w11 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w12 = new Gtk.HBox();
            w12.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w13 = new Gtk.Image();
            w13.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-add", Gtk.IconSize.Menu, 16);
            w12.Add(w13);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w15 = new Gtk.Label();
            w15.LabelProp = Mono.Unix.Catalog.GetString("New Dock");
            w15.UseUnderline = true;
            w12.Add(w15);
            w11.Add(w12);
            this.new_dock_button.Add(w11);
            this.hbox3.Add(this.new_dock_button);
            Gtk.Box.BoxChild w19 = ((Gtk.Box.BoxChild)(this.hbox3[this.new_dock_button]));
            w19.Position = 0;
            w19.Expand = false;
            w19.Fill = false;
            // Container child hbox3.Gtk.Box+BoxChild
            this.delete_dock_button = new Gtk.Button();
            this.delete_dock_button.CanFocus = true;
            this.delete_dock_button.Name = "delete_dock_button";
            this.delete_dock_button.UseUnderline = true;
            this.delete_dock_button.BorderWidth = ((uint)(5));
            // Container child delete_dock_button.Gtk.Container+ContainerChild
            Gtk.Alignment w20 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w21 = new Gtk.HBox();
            w21.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w22 = new Gtk.Image();
            w22.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-delete", Gtk.IconSize.Menu, 16);
            w21.Add(w22);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w24 = new Gtk.Label();
            w24.LabelProp = Mono.Unix.Catalog.GetString("Delete Dock");
            w24.UseUnderline = true;
            w21.Add(w24);
            w20.Add(w21);
            this.delete_dock_button.Add(w20);
            this.hbox3.Add(this.delete_dock_button);
            Gtk.Box.BoxChild w28 = ((Gtk.Box.BoxChild)(this.hbox3[this.delete_dock_button]));
            w28.Position = 1;
            w28.Expand = false;
            w28.Fill = false;
            // Container child hbox3.Gtk.Box+BoxChild
            this.vbox4 = new Gtk.VBox();
            this.vbox4.Name = "vbox4";
            this.vbox4.Spacing = 6;
            this.hbox3.Add(this.vbox4);
            Gtk.Box.BoxChild w29 = ((Gtk.Box.BoxChild)(this.hbox3[this.vbox4]));
            w29.Position = 2;
            this.hbox2.Add(this.hbox3);
            Gtk.Box.BoxChild w30 = ((Gtk.Box.BoxChild)(this.hbox2[this.hbox3]));
            w30.Position = 0;
            // Container child hbox2.Gtk.Box+BoxChild
            this.close_button = new Gtk.Button();
            this.close_button.CanFocus = true;
            this.close_button.Name = "close_button";
            this.close_button.UseStock = true;
            this.close_button.UseUnderline = true;
            this.close_button.BorderWidth = ((uint)(5));
            this.close_button.Label = "gtk-close";
            this.hbox2.Add(this.close_button);
            Gtk.Box.BoxChild w31 = ((Gtk.Box.BoxChild)(this.hbox2[this.close_button]));
            w31.Position = 1;
            w31.Expand = false;
            w31.Fill = false;
            this.vbox1.Add(this.hbox2);
            Gtk.Box.BoxChild w32 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
            w32.Position = 1;
            w32.Expand = false;
            w32.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 488;
            this.DefaultHeight = 482;
            this.Show();
            this.start_with_computer_checkbutton.Toggled += new System.EventHandler(this.OnStartWithComputerCheckbuttonToggled);
            this.theme_combo.Changed += new System.EventHandler(this.OnThemeComboChanged);
            this.new_dock_button.Clicked += new System.EventHandler(this.OnNewDockButtonClicked);
            this.delete_dock_button.Clicked += new System.EventHandler(this.OnDeleteDockButtonClicked);
            this.close_button.Clicked += new System.EventHandler(this.OnCloseButtonClicked);
        }
    }
}
