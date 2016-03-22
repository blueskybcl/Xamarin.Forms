﻿using System;

using Xamarin.Forms.CustomAttributes;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
#endif

namespace Xamarin.Forms.Controls
{
	[Preserve (AllMembers = true)]
	[Issue (IssueTracker.Bugzilla, 29453, "Navigation.PopAsync(false) in Entry.Completed handler => System.ArgumentException", PlatformAffected.Android)]
	public class Bugzilla29453 : TestContentPage // or TestMasterDetailPage, etc ...
	{
		protected override void Init ()
		{
			var page1Layout = new StackLayout {
				Children = {
					new Label {
						XAlign = TextAlignment.Center,
						Text = "Page 1"
					}
				}
			};

			var page2Layout = new StackLayout {
				Children = {
					new Label {
						XAlign = TextAlignment.Center,
						Text = "Page 2"
					}
				}
			};

			var entry = new Entry { AutomationId = "entryText" };

			entry.Completed += async (sender, args) => {
				await Navigation.PopAsync (false);
			};

			page2Layout.Children.Add (entry);

			var page2 = new ContentPage {
				Content = page2Layout
			};

			var button = new Button {
				Text = "Go to page 2",
				AutomationId = "btnGotoPage2",
				Command = new Command (async () => await Navigation.PushAsync (page2))
			};

			page1Layout.Children.Add (button);
			Content = page1Layout;
		}

		#if UITEST
		[Test]
		public void Bugzilla29453Test ()
		{
			RunningApp.Screenshot ("I am at Issue Bugzilla29453");
			RunningApp.WaitForElement (q => q.Marked ("Page 1"));
			RunningApp.Tap (q => q.Marked ("btnGotoPage2"));
			RunningApp.Tap (q => q.Marked ("entryText"));
			RunningApp.EnterText ("XF");
			RunningApp.PressEnter ();
			RunningApp.WaitForElement (q => q.Marked ("Page 1"));
		}
#endif
	}
}
