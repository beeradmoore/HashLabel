using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HashLabelTest
{
	public class HashLabel : UILabel
	{
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				UpdateHashs();
			}
		}

		public override RectangleF Frame
		{
			get
			{
				return base.Frame;
			}
			set
			{
				base.Frame = value;
				UpdateHashs();
			}
		}

		private NSMutableAttributedString _attributedString = null;

		private List<RectangleF> _rectangles = new List<RectangleF>();


		public HashLabel(RectangleF frame) : base(frame)
		{
			Initialize();
		}
		
		public HashLabel() : base()
		{
			Initialize();
		}
		
		public HashLabel(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		public HashLabel(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		public HashLabel(NSObjectFlag t) : base(t)
		{
			Initialize();
		}

		private void Initialize()
		{
			this.UserInteractionEnabled = true;
		}

		private void UpdateHashs()
		{
			if (base.Text == null)
				return;

			_attributedString = new NSMutableAttributedString(base.Text);

			Regex hashTag = new Regex(@"(#)((?:[A-Za-z0-9-_]*))");


			_rectangles.Clear();
			//_rectangles.Add(new RectangleF(0, 0, 10, this.Font.LineHeight));
			//_rectangles.Add(new RectangleF(20, 0, 10, this.Font.LineHeight));
		
		
			//NSMutableAttributedString attributedString = new NSMutableAttributedString(username + " is " + _items[indexPath.Row - 1].mood_id);
			//attributedString.AddAttribute(UIStringAttributeKey.Font, boldFont, new NSRange(0, username.Length));
			if (base.Text.Length > 0)
			{
				MatchCollection matchCollection = hashTag.Matches(base.Text);


				if (matchCollection.Count > 0)
				{
					List<string> lines = new List<string>();
					List<bool> wordWrapped = new List<bool>();

					List<string> words = new List<string>();
					foreach (string word in base.Text.Split(' '))
					{
						words.Add(word);
					}


					string tempLine1 = String.Empty;
					for (int i = 0; i < words.Count; ++i)
					{
						string tempLine2 = String.Empty;

						if (tempLine1 == String.Empty)
						{
							tempLine2 = words[i];
						}
						else
						{
							tempLine2 = tempLine1 + " " + words[i];
						}


						if (this.StringSize(tempLine2, this.Font).Width > this.Frame.Width)
						{
							if (this.StringSize(words[i], this.Font).Width > this.Frame.Width)
							{
								lines.Add(tempLine1);							
								wordWrapped.Add(false);
								tempLine1 = String.Empty;
								
								string tempWord = tempLine1;

								for (int letter_index = 0; letter_index < words[i].Length; ++letter_index)
								{
									string newTempWord = tempWord + words[i][letter_index];

									if (this.StringSize(newTempWord, this.Font).Width > this.Frame.Width)
									{
										//tempLine2 = tempLine1 + " " + tempWord;

										lines.Add(tempWord);
										wordWrapped.Add(true);
										tempLine1 = words[i].Substring(letter_index);
										break;
									}
									else
									{
										tempWord = newTempWord;
									}
								}
							}
							else
							{
								wordWrapped.Add(false);
								lines.Add(tempLine1);
								tempLine1 = words[i];
							}
						}
						else
						{
							tempLine1 = tempLine2;
						}
					}

					if (tempLine1.Length > 0)
					{
						wordWrapped.Add(false);
						lines.Add(tempLine1);
					}


					foreach (Match match in matchCollection)
					{					
						_attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, UIColor.Red, new NSRange(match.Index, match.Length));
					}

					Console.WriteLine("##################");
					for (int i = 0; i < lines.Count; ++i)
					{
						if (wordWrapped[i])
						{
							MatchCollection newMatchCollection = hashTag.Matches(lines[i]);


							foreach (Match match in newMatchCollection)
							{	
								float left = this.StringSize(lines[i].Substring(0, match.Index), this.Font).Width;
								float top = this.Font.LineHeight * i;
								float width = this.StringSize(match.Value, this.Font).Width;
								float height = this.Font.LineHeight;
								
								_rectangles.Add(new RectangleF(left, top, width, height));
								//_attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, UIColor.Red, new NSRange(match.Index, match.Length));
							}

							int lastIndex = lines[i].LastIndexOf(' ');
							int firstIndex = lines[i+1].IndexOf(' ');

							//if (lastIndex == -1)
							//	lastIndex = 0;

							if (firstIndex == -1)
								firstIndex = lines[i+1].Length;

							// TODO: Check if this +1 is needed, or if "lastIndex" is -1 shoudl onyl become 0
							string wrappedWord = lines[i].Substring(lastIndex + 1) + lines[i+1].Substring(0, firstIndex);
							Console.WriteLine("Wrapped: |" + wrappedWord + "|");

							Match wrapMatch = hashTag.Match(wrappedWord);
							if (wrapMatch.Success)
							{
								float left = 0;
								float top = this.Font.LineHeight * (i + 1);
								float width = this.StringSize(wrapMatch.Value.Substring(lines[i].Length - (lastIndex + 1)), this.Font).Width;
								float height = this.Font.LineHeight;
								
								_rectangles.Add(new RectangleF(left, top, width, height));
							}
						}
						else
						{
							MatchCollection newMatchCollection = hashTag.Matches(lines[i]);

							foreach (Match match in newMatchCollection)
							{	
								float left = this.StringSize(lines[i].Substring(0, match.Index), this.Font).Width;
								float top = this.Font.LineHeight * i;
								float width = this.StringSize(match.Value, this.Font).Width;
								float height = this.Font.LineHeight;

								_rectangles.Add(new RectangleF(left, top, width, height));
								//_attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, UIColor.Red, new NSRange(match.Index, match.Length));
							}
						}
						Console.WriteLine(wordWrapped[i] + " " + lines[i]);
					}

				}
			}



			this.AttributedText = _attributedString;

			//attributedString.AddAttribute(CTStringAttributeKey.StrokeColor, UIColor.Blue, new NSRange(0, 3));
			//cell.LabelPoster.AttributedText = attributedString;
			//cell.LabelPoster.SizeToFit();
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);

			UpdateHashs();

			//Console.WriteLine("TOUCHED - " + );
			foreach (UITouch touch in touches)
			{

				Console.WriteLine("TOUCHED - " + touch.LocationInView(this));
			}



		}

		public override void Draw(RectangleF rect)
		{

			using (CGContext gctx = UIGraphics.GetCurrentContext())
			{
				//set up drawing attributes
				UIColor.Blue.SetFill();
				
				//create geometry
				CGPath path = new CGPath();

				foreach (RectangleF rectangle in _rectangles)
				{
					path.AddRect(rectangle);
				}
				path.CloseSubpath();
				
				//add geometry to graphics context and draw it
				gctx.AddPath(path);        
				gctx.DrawPath(CGPathDrawingMode.FillStroke);   
			}  

			base.Draw (rect);



			/*
			CGContext oContext = UIGraphics.GetCurrentContext();
			oContext.SetStrokeColor(UIColor.Red.CGColor.Components);
			oContext.SetLineWidth(3.0f);
			oContext.FillRect(new RectangleF(0, 0, 40, 40));
			//this.oLastPoint.Y = UIScreen.MainScreen.ApplicationFrame.Size.Height - this.oLastPoint.Y;
			//this.oCurrentPoint.Y = UIScreen.MainScreen.ApplicationFrame.Size.Height - this.oCurrentPoint.Y;
			//oContext.StrokeLineSegments(new PointF[] {this.oLastPoint, this.oCurrentPoint });
			oContext.Flush();
			oContext.RestoreState();
		///	Console.Out.WriteLine("Current X: {0}, Y: {1}", oCurrentPoint.X.ToString(), oCurrentPoint.Y.ToString());
			//Console.Out.WriteLine("Last X: {0}, Y: {1}", oLastPoint.X.ToString(), oLastPoint.Y.ToString());*/
		}



	}
}

