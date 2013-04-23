using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace HashLabelTest
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UIViewController viewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new UIViewController();

			viewController.View.BackgroundColor = UIColor.White;


			/*
			HashLabel label1 = new HashLabel(new RectangleF(10, 10, 300, 300));
			label1.Text = "This is my #test number #one";
			label1.Lines = 0;
			label1.Frame = new RectangleF(label1.Frame.Location, label1.SizeThatFits(new SizeF(300, 900)));
			viewController.View.Add(label1);
			
			HashLabel label2 = new HashLabel(new RectangleF(10, 100, 300, 300));
			label2.Text = "This is my #test number #two of some longer text for me to #test with. I hope it works out.";
			label2.Lines = 0;
			label2.Frame = new RectangleF(label2.Frame.Location, label2.SizeThatFits(new SizeF(300, 900)));
			viewController.View.Add(label2);
			
			HashLabel label3 = new HashLabel(new RectangleF(10, 200, 300, 300));
			label3.Text = "This is my #test number #one. Its only slightly longer, but not as wide.";
			label3.Lines = 0;
			label3.Frame = new RectangleF(label3.Frame.Location, label3.SizeThatFits(new SizeF(150, 900)));
			viewController.View.Add(label3);
			*/

			
			HashLabel label4 = new HashLabel(new RectangleF(10, 70, 300, 300));
			label4.Text = "This is my #test number #four. #testingAReallyLongHashTag. Its only slightly longer, but not as wide.";
			label4.Lines = 0;
			label4.Frame = new RectangleF(label4.Frame.Location, label4.SizeThatFits(new SizeF(150, 900)));
			viewController.View.Add(label4);


			UISlider slider = new UISlider(new RectangleF(0, 50, 320, 15));
			slider.MinValue = 0;
			slider.MaxValue = 320;
			slider.Value = 150;
			slider.ValueChanged += delegate(object sender, EventArgs e) {
				UISlider tempSlider = sender as UISlider;

				label4.Frame = new RectangleF(label4.Frame.Location, label4.SizeThatFits(new SizeF(tempSlider.Value, 900)));

			};
			viewController.View.Add(slider);








			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

