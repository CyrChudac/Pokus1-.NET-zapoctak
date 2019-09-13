using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;

namespace CoreLib
{
	public interface IAnimation
	{
		[JsonIgnore]
		Image Image { get; }
		/// <summary>
		/// Should be called every frame. Let's the animation to change curr picture.
		/// </summary>
		void Update();
	}
	/// <summary>
	/// Animates every animated game object.
	/// </summary>
	public class Animation : IAnimation
	{
		private static string animFileName => throw new NotImplementedException(); 
		public Animation(float picChangeDelay, string animImagesNameInAnimFile)
		{
			this.picChangeDelay = picChangeDelay;
			this.animImagesNameInAnimFile = animImagesNameInAnimFile;
			foreach (var l in Directory.GetFiles(
				Directory.GetCurrentDirectory() + @"\" + animFileName
				).Select(x => x.Contains(animImagesNameInAnimFile)))
				pictures.Add(Image.FromFile(Directory.GetCurrentDirectory() + @"\" + l));
		}
		[JsonRequired]
		public readonly string animImagesNameInAnimFile;
		//public Animation(float picPerSec, List<Image> pictures)
		//{
		//	this.pictures = pictures;
		//	this.picChangeSpeed = 1000 / picPerSec;
		//	this.lastChangeTime = Time.Now;
		//}
		[JsonIgnore]
		readonly List<Image> pictures = new List<Image>();
		[JsonRequired]
		int currPic = 0;
		[JsonRequired]
		readonly float picChangeDelay;
		[JsonRequired]
		long lastChangeTime = 0;

		/// <summary>
		/// Picture that suits current animation state.
		/// </summary>
		[JsonIgnore]
		public Image Image => pictures[currPic];

		/// <summary>
		/// should be called every frame, let's animation to change pictures
		/// </summary>
		public void Update()
		{
			if (lastChangeTime + picChangeDelay > Time.Now)
			{
				currPic = (currPic + 1) % pictures.Count;
				lastChangeTime = Time.Now;
			} 
		}
	}
	public sealed class NoAnimation : IAnimation
	{
		private NoAnimation() { }
		/// <summary>
		/// Just a fake animation that let's the developper to apply it while he has no image for given object.
		/// </summary>
		public static readonly NoAnimation Singleton = new NoAnimation();
		public Image Image => null;
		public void Update() { }
	}
	public class SingleColorAnimation : IAnimation
	{
		public static readonly Size imageDefaultSize = new Size(20, 20);	
		[JsonConstructor]
		public SingleColorAnimation(Color color)
		{
			image = new Bitmap(imageDefaultSize.Width, imageDefaultSize.Height);
			for (int i = 0; i < imageDefaultSize.Width; i++)
				for (int j = 0; j < imageDefaultSize.Height; j++)
					image.SetPixel(i, j, color);
			this.color = color;
		}
		[JsonRequired]
		readonly Color color;
		readonly Bitmap image;
		public Image Image => image;

		public void Update() { }
	}
	public class DefaultDeadAnimation : SingleColorAnimation
	{
		private DefaultDeadAnimation() : base(Color.LightSlateGray) { }
		public static readonly DefaultDeadAnimation instance = new DefaultDeadAnimation();
	}
}
