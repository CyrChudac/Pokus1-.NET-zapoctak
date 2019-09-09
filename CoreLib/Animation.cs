using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CoreLib
{
	public interface IAnimation
	{
		System.Drawing.Image Image { get; }
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
		public Animation(float picChangeSpeed, string animImagesNameInAnimFile)
		{
			throw new NotImplementedException();
		}
		public Animation(float picPerSec, List<Image> pictures)
		{
			this.pictures = pictures;
			this.picChangeSpeed = 1000 / picPerSec;
			this.lastChangeTime = Time.Now;
		}
		readonly List<Image> pictures;

		int currPic = 0;
		readonly float picChangeSpeed;
		long lastChangeTime;

		/// <summary>
		/// Picture that suits current animation state.
		/// </summary>
		public Image Image => pictures[currPic];

		/// <summary>
		/// should be called every frame, let's animation to change pictures
		/// </summary>
		public void Update()
		{
			if (lastChangeTime + picChangeSpeed > Time.Now)
			{
				currPic = (currPic + 1) % pictures.Count;
				lastChangeTime = Time.Now;
			} 
		}
	}
	sealed class NoAnimation : IAnimation
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
		public SingleColorAnimation(Color color)
		{
			image = new Bitmap(1, 1);
			image.SetPixel(0, 0, color);
		}
		readonly Bitmap image;
		public Image Image => image;

		public void Update() {}
	}
	public class DefaultDeadAnimation : SingleColorAnimation
	{
		private DefaultDeadAnimation() : base(Color.LightSlateGray) { }
		public static readonly DefaultDeadAnimation instance = new DefaultDeadAnimation();
	}
}
