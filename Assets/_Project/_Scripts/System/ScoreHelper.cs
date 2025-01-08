using System;
using UnityEngine;

namespace Smash.System
{
	public class ScoreHelper
	{
		/// <summary>
		///		Converts milliseconds to minutes and seconds
		/// </summary>
		/// <param name="score"></param>
		/// <returns></returns>
		public static string ConvertScore(double score)
		{
			int minutes = (int)(score / 60000f);
			int seconds = (int)((score % 60000f) / 1000f);
			int milliSeconds = (int)(score % 1000f);
			return $"{minutes:00}:{seconds:00}.{milliSeconds:000}";
		}

		private const double k_Default_Score = 999999999999;
		public static double DefaultScore => k_Default_Score;
		
		public static bool ApproximatelyEqual(double a, double b)
		{
			return Math.Abs(b - a) < Math.Max(1E-06f * Math.Max(Math.Abs(a), Math.Abs(b)), Mathf.Epsilon * 8f);
		}
	}
}
