using C1.C1Pdf;
using C1.Win.C1Tile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace ImageGalleryDemo1
{
    public partial class ImageGallery : Form

    {   
        DataFetcher datafetch = new DataFetcher();   // add an instance of DataFechter class
        List<ImageItem> imagesList;                 // add an instance of list of ImageItem class.
        int checkedItems = 0;                       // variable to keep a count of the checked tiles.

        // Initialize the two panel dynamically.
       
        Panel panelFirst = new Panel();
        Panel panelSecond = new Panel();

        // Initialize text box dynamically.

        TextBox textbox = new TextBox();

        // Initialize PictureBox to add images and label for Search , Export and Save.

        PictureBox picturebox_Search = new PictureBox();
        PictureBox picturebox_Export = new PictureBox();
        PictureBox picturebox_Save = new PictureBox();

        // Initialize Label for Export and Save.

        Label label_Export = new Label();
        Label label_Save = new Label();

        // Initialize C1TileControl .

        C1TileControl tile = new C1TileControl();

        // Initialize SatatusStrip for showing the status.

        StatusStrip statusStrip = new StatusStrip();

        // Initialize Tile for split up the column.

        Tile tile1 = new Tile();
        Tile tile2 = new Tile();
        Tile tile3 = new Tile();

        // Initialize Image and text element.

        ImageElement imageElement = new ImageElement();
        TextElement textElement = new TextElement();

        // Initialize group for grouping all the tiles.

        Group _group = new Group();

        // Initialize ProgressBar for showing the progress.

        ToolStripProgressBar tSPB = new ToolStripProgressBar();

        // Initialize Splitcontainer for spliting up the form.

        SplitContainer splitContainer = new SplitContainer();

        // Initialize Tablelayoutpanel for adding rows and coloumn.

        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();

        // Initialize C1PdfDocument for image to pdf converter.

        C1PdfDocument imagePdfDocument = new C1PdfDocument();

        public ImageGallery()
        {
            InitializeComponent();

            // Changing the proprties of the Form.

            MaximumSize = new Size(810, 810);
            MaximizeBox = false;
            ShowIcon = false;
            Size = new Size(780, 700);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Image Gallery";
            BackColor = Color.White;

            // Splitting up the form by using Splitcontainer.

            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Margin = new Padding(2);
            splitContainer.SplitterDistance = 40;
            splitContainer.FixedPanel = FixedPanel.Panel1;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Orientation = Orientation.Horizontal;
            splitContainer.Panel1.Controls.Add(tableLayoutPanel);
            splitContainer.Panel2.Controls.Add(picturebox_Export);
            splitContainer.Panel2.Controls.Add(label_Export);
            splitContainer.Panel2.Controls.Add(label_Save);
            splitContainer.Panel2.Controls.Add(picturebox_Save);
            splitContainer.Panel2.Controls.Add(tile);
            splitContainer.Panel2.Controls.Add(statusStrip);
            Controls.Add(splitContainer);

            // Serach Box creation using textbox and changing some of its properties.

            textbox.Name = "_searchBox";
            textbox.BorderStyle = BorderStyle.None;
            textbox.Dock = DockStyle.Fill;
            textbox.Location = new Point(16, 9);
            textbox.Size = new Size(244, 16);
            textbox.Text = "Search Image";
            textbox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);

            // Search Button creation using Picturebox , changing some of its properties and also add  event handler .

            picturebox_Search.Name = "_search";
            picturebox_Search.Dock = DockStyle.Left;
            picturebox_Search.Location = new Point(0, 0);
            picturebox_Search.Margin = new Padding(0, 0, 0, 0);
            picturebox_Search.Size = new Size(40, 16);
            picturebox_Search.SizeMode = PictureBoxSizeMode.Zoom;
            picturebox_Search.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            picturebox_Search.Image = Properties.Resources.Search;
            picturebox_Search.Click += new EventHandler(OnSearchClick);

            // Export Image to PDF Button creation using Picturebox , changing some of its properties and  also add click and paint event handler .

            picturebox_Export.Name = "_exportImage";
            picturebox_Export.Location = new Point(29, 1);
            picturebox_Export.Size = new Size(75, 25);
            picturebox_Export.SizeMode = PictureBoxSizeMode.Zoom;
            picturebox_Export.Visible = false;
            picturebox_Export.Image = Properties.Resources.ExportToPDF;
            picturebox_Export.Click += new EventHandler(On_ExportClick);
            picturebox_Export.Paint += new PaintEventHandler(OnExportImagePaint);

            // Generate Export Image to PDF label using label and changing some of its properties.

            label_Export.Name = "_exportLabel";
            label_Export.Text = "Export to PDF";
            label_Export.Location = new Point(29, 28);
            label_Export.Size = new Size(75, 15);
            label_Export.Visible = false;

            // Save to Local button creation using Picturebox , changing some of its properties and also add click and paint event handler .

            picturebox_Save.Name = "_saveImage";
            picturebox_Save.Location = new Point(120, 1);
            picturebox_Save.Size = new Size(85, 28);
            picturebox_Save.SizeMode = PictureBoxSizeMode.Zoom;
            picturebox_Save.Image = Properties.Resources.SaveToPC;
            picturebox_Save.Visible = false;
            picturebox_Save.Click += new EventHandler(onsavePcClick);
            picturebox_Save.Paint += new PaintEventHandler(onsavePcImagePaint);

            // Generating Save to Local label using label and changing some of its properties.

            label_Save.Name = "_saveLocal";
            label_Save.Text = "Save to Local";
            label_Save.Location = new Point(129, 28);
            label_Save.Size = new Size(75, 15);
            label_Save.Visible = false;

            // Add tabel in PanelSecond using tableLayoutpanel and changing some of its properties.

            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.Size = new Size(800, 40);
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(panelFirst, 1, 0);
            tableLayoutPanel.Controls.Add(panelSecond, 2, 0);

            // Adding panelFirst and Second and also add  paint event handler for panelfirst.

            panelFirst.Location = new Point(477, 0);
            panelFirst.Size = new Size(287, 40);
            panelFirst.Dock = DockStyle.Fill;
            panelFirst.Paint += new PaintEventHandler(OnSearchPanelPaint);
            panelFirst.Controls.Add(textbox);
            panelSecond.Size = new Size(40, 16);
            panelSecond.Location = new Point(479, 12);
            panelSecond.Margin = new Padding(2, 12, 45, 12);
            panelSecond.TabIndex = 1;
            panelSecond.Controls.Add(picturebox_Search);

            // Changing some the proprties of Tiles.

            tile1.BackColor = Color.LightCoral;
            tile1.Name = "tileOne";
            tile1.Text = "Tile One";

            tile2.BackColor = Color.Teal;
            tile2.Name = "tiletwo";
            tile2.Text = "Tile Two";

            tile3.BackColor = Color.SteelBlue;
            tile3.Name = "tilethree";
            tile3.Text = "Tile Three";

            //Using group , Grouping all tiles.

            _group.Name = "_group";
            _group.Text = "Results";
            _group.Tiles.Add(tile1);
            _group.Tiles.Add(tile2);
            _group.Tiles.Add(tile3);
            _group.Visible = false;

            // Changing some the properties of Tiles.

            tile.Name = "image_tileControl";
            tile.AllowChecking = true;
            tile.AllowRearranging = true;
            tile.CellHeight = 78;
            tile.CellSpacing = 11;
            tile.CellWidth = 78;
            tile.Dock = DockStyle.Fill;
            tile.Size = new Size(764, 718);
            tile.SurfacePadding = new Padding(12, 4, 12, 4);
            tile.SwipeDistance = 20;
            tile.SwipeRearrangeDistance = 98;
            tile.Groups.Add(_group);
            tile.Location = new Point(0, 0);
            tile.Orientation = LayoutOrientation.Vertical;
            tile.TileChecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(OnTileChecked);
            tile.TileUnchecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(OnTileUnchecked);
            tile.Paint += new PaintEventHandler(OnTileControlPaint);

            // Changing some the properties Status Bar

            statusStrip.Dock = DockStyle.Bottom;
            statusStrip.Visible = false;
            tSPB.Style = ProgressBarStyle.Marquee;
            statusStrip.Items.Add(tSPB);

        }

        // Now adding method of the event handler.



        // Event Handler for Tilecontrolpaint.
        private void OnTileControlPaint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }

        // Event Handler for TileUnchecked.
        private void OnTileUnchecked(object sender, TileEventArgs e)
        {
            checkedItems--;
            picturebox_Export.Visible = checkedItems > 0;
            picturebox_Save.Visible = checkedItems > 0;
            label_Export.Visible = checkedItems > 0;
            label_Save.Visible = checkedItems > 0;
        }

        // Event Handler for TileChecked.
        private void OnTileChecked(object sender, TileEventArgs e)
        {
            checkedItems++;
            picturebox_Export.Visible = true;
            picturebox_Save.Visible = true;
            label_Export.Visible = true;
            label_Save.Visible = true;
        }

        // Event Handler for SearchPanel paint.
        private void OnSearchPanelPaint(object sender, PaintEventArgs e)
        {
            Rectangle r = textbox.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
        }

        // Event Handler for save Local image paint.
        private void onsavePcImagePaint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(picturebox_Save.Location.X, picturebox_Save.Location.Y, picturebox_Save.Width, picturebox_Save.Height);
            r.X -= 29;
            r.Y -= 1;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new Point(Width, 43));
        }


        // Event Handler for save local image click Button.
        private void onsavePcClick(object sender, EventArgs e)
        {
            List<Image> images1 = new List<Image>();
            foreach (Tile tile_ in tile.Groups[0].Tiles)
            {
                if (tile_.Checked)
                {
                    images1.Add(tile_.Image);
                }
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "jpg";
            saveFile.Filter = "JPG files (*.jpg)|*.jpg*";
            foreach (var img in images1)
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    img.Save(saveFile.FileName);
                }
            }
        }


        // Event Handler for Export Image to PDF paint.
        private void OnExportImagePaint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(picturebox_Export.Location.X, picturebox_Export.Location.Y, picturebox_Export.Width, picturebox_Export.Height);
            r.X -= 29;
            r.Y -= 1;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.White);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new Point(Width, 43));
        }

        // Event Handler for Export Image to Pdf click button.
        private void On_ExportClick(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile tile_ in tile.Groups[0].Tiles)
            {
                if (tile_.Checked)
                {
                    images.Add(tile_.Image);
                }
            }
            ConvertToPdf(images);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "pdf";
            saveFile.Filter = "PDF files (*.pdf)|*.pdf*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                imagePdfDocument.Save(saveFile.FileName);
            }
        }

        // Method for Convert Image to PDF.
        private void ConvertToPdf(List<Image> images)
        {
            RectangleF rect = imagePdfDocument.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    imagePdfDocument.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                imagePdfDocument.DrawImage(selectedimg, rect);
            }
        }

        // Event Handler for Search Click Button.
        private async void OnSearchClick(object sender, EventArgs e)
        {
           
            statusStrip.Visible = true;
            imagesList = await datafetch.GetImageData(textbox.Text);
            AddTiles(imagesList);
            statusStrip.Visible = false;
        }

        // Method for Add Tiles.
        private void AddTiles(List<ImageItem> imagesList)
        {
            tile.Groups[0].Tiles.Clear();
            _group.Visible = true;
            foreach (var imageitem in imagesList)
            {
                Tile tile_ = new Tile();
                tile_.HorizontalSize = 2;
                tile_.VerticalSize = 2;
                tile.Groups[0].Tiles.Add(tile_);
                Image img = Image.FromStream(new MemoryStream(imageitem.Base64));
                Template tl = new Template();
                ImageElement imageElement = new ImageElement();
                imageElement.ImageLayout = ForeImageLayout.Stretch;
                tl.Elements.Add(imageElement);
                tile_.Template = tl;
                tile_.Image = img;
            }
        }
    }
}
