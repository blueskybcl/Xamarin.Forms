﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.CustomAttributes;

namespace Xamarin.Forms.Controls
{
	[Preserve (AllMembers=true)]
	[Issue (IssueTracker.None, 0, "Complex ListView", PlatformAffected.All)]
	public class ComplexListView
		: ContentPage
	{
		public ComplexListView()
		{
			Performance.Clear();

			var showPerf = new Button { Text = "Performance" };
			showPerf.Clicked += (sender, args) => {
				Performance.DumpStats();
				Performance.Clear();
			};

			Content = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Children = {
					showPerf,
					new ListView {
						ItemTemplate = new DataTemplate (typeof (ComplexViewCell)),
						ItemsSource =
							new[] {
								"a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a",
								"b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c", "a", "b", "c"
							}
					}
				}
			};
		}
	}

	internal class ComplexViewCell
		: ViewCell
	{
		static ImageSource s_mImgProdCount;
		static ImageSource s_mImgEndTime;
		static ImageSource s_mImgRenewal;

		public ComplexViewCell()
		{
			if (s_mImgProdCount == null)
				s_mImgProdCount = ImageSource.FromFile ("bank.png");
			if (s_mImgEndTime == null)
				s_mImgEndTime = ImageSource.FromFile ("bank.png");
			if (s_mImgRenewal == null)
				s_mImgRenewal = ImageSource.FromFile ("bank.png");

			var label1 = new Label { Text = "Label 1", Font = Font.SystemFontOfSize (NamedSize.Small, FontAttributes.Bold) };
			label1.SetBinding (Label.TextProperty, new Binding ("."));

			var label2 = new Label { Text = "Label 2", Font = Font.SystemFontOfSize (NamedSize.Small) };

			// was ListButton?
			var button = new Button {
				Text = "X",
				BackgroundColor = Color.Gray,
				HorizontalOptions = LayoutOptions.EndAndExpand
			};
			button.SetBinding (Button.CommandParameterProperty, new Binding ("."));
			button.Clicked += (sender, e) => {
				var b = (Button) sender;
				var t = b.CommandParameter;
				((ContentPage) ((ListView) ((StackLayout) b.ParentView).ParentView).ParentView).DisplayAlert ("Clicked",
					t + " button was clicked", "OK");
				Debug.WriteLine ("clicked" + t);
			};

			Image imgProdCount = new Image {
				Aspect = Aspect.AspectFit,
				Source = s_mImgProdCount,
			};

			Image imgEndTime = new Image {
				Aspect = Aspect.AspectFit,
				Source = s_mImgEndTime,
			};

			Image imgRenewal = new Image {
				Aspect = Aspect.AspectFit,
				Source = s_mImgRenewal,
			};

			View = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness (15, 5, 5, 15),
				Children = {
					new StackLayout {
						Orientation = StackOrientation.Vertical,
						Children = { label1, label2 }
					},
					button,
					imgProdCount,
					imgEndTime,
					imgRenewal
				}
			};
		}
	}
}
