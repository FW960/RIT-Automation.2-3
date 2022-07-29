using System;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Task._1.Db;
using Task._1.Entities;

namespace Task._1
{
    public partial class Form1 : Form
    {
        private MyDatabase Database { get; set; }
        private GMapMarker LastMarkerClicked { get; set; }

        private GMapOverlay GMapMarkers { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode =
                AccessMode.ServerAndCache;
            gMapControl1.MapProvider =
                GMap.NET.MapProviders.GoogleMapProvider
                    .Instance;
            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 16;
            gMapControl1.Zoom = 4;
            gMapControl1.Position =
                new PointLatLng(66.4169575018027,
                    94.25025752215694);
            gMapControl1.MouseWheelZoomType =
                MouseWheelZoomType
                    .MousePositionAndCenter;
            gMapControl1.CanDragMap = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.ShowCenter = false;
            gMapControl1.ShowTileGridLines = false;
            Database = new MyDatabase();
            gMapControl1.Overlays.Add(GetOverlayMarkers("Overlay"));
        }

        private GMarkerGoogle GetMarker(MyMarker myMarker)
        {
            GMarkerGoogle mapMarker = new GMarkerGoogle(new PointLatLng(myMarker.Latitude, myMarker.Longitude), GMarkerGoogleType.red);
            mapMarker.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(mapMarker);
            mapMarker.ToolTipText = myMarker.Name;
            mapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            return mapMarker;
        }

        private GMapOverlay GetOverlayMarkers(string name)
        {
            GMapMarkers = new GMapOverlay(name); 

            foreach (var marker in Database.GetAll())
            {
                GMapMarkers.Markers.Add(GetMarker(marker));
            }

            return GMapMarkers;
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            LastMarkerClicked = item;
        }

        private void gMapControl1_OnMapClick(PointLatLng pointClick, MouseEventArgs e)
        {
            if (LastMarkerClicked != null)
            {
                int count = 0;
                
                foreach (var marker in GMapMarkers.Markers)
                {
                    if (marker.ToolTipText == LastMarkerClicked.ToolTipText)
                    {
                        GMapMarkers.Markers.RemoveAt(count);
                        MyMarker myMarker = new MyMarker
                        {
                            Name = marker.ToolTipText,
                            Latitude = pointClick.Lat,
                            Longitude = pointClick.Lng
                        };
                        GMapMarkers.Markers.Add(GetMarker(myMarker));
                        
                        Database.Update(myMarker);
                        LastMarkerClicked = null;
                        return;
                    }
                    count++;
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Size s = new Size();

            Point p = Point.Empty;

            p.X = 10;

            p.Y = 10;
            
            s.Height = ClientRectangle.Height - 20;

            s.Width = ClientRectangle.Width - 20;
            
            gMapControl1.Location = p;
            
            gMapControl1.Size = s;
        }
    }
}