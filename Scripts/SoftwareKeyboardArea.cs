using UnityEngine;

namespace UniSoftwareKeyboardArea
{
	/// <summary>
	/// ソフトウェアキーボードの表示領域を管理するクラス
	/// </summary>
	public static class SoftwareKeyboardArea
	{
		//================================================================================
		// プロパティ(static)
		//================================================================================
		/// <summary>
		/// 高さを返します
		/// </summary>
		public static int GetHeight()
		{
			return GetHeight( false );
		}

		/// <summary>
		/// 高さを返します
		/// </summary>
		public static int GetHeight( bool includeInput )
		{
#if !UNITY_EDITOR && UNITY_ANDROID
			using ( var unityClass = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
			{
				var currentActivity = unityClass.GetStatic<AndroidJavaObject>( "currentActivity" );
				var unityPlayer = currentActivity.Get<AndroidJavaObject>( "mUnityPlayer" );
				var view = unityPlayer.Call<AndroidJavaObject>( "getView" );

				if ( view == null ) return 0;

				int result;

				using ( var rect = new AndroidJavaObject( "android.graphics.Rect" ) )
				{
					view.Call( "getWindowVisibleDisplayFrame", rect );
					result = Screen.height - rect.Call<int>( "height" );
				}

				if ( !includeInput ) return result;

				var softInputDialog = unityPlayer.Get<AndroidJavaObject>( "mSoftInputDialog" );
				var window = softInputDialog?.Call<AndroidJavaObject>( "getWindow" );
				var decorView = window?.Call<AndroidJavaObject>( "getDecorView" );

				if ( decorView == null ) return result;

				var decorHeight = decorView.Call<int>( "getHeight" );
				result += decorHeight;

				return result;
			}
#else
			var area   = TouchScreenKeyboard.area;
			var height = Mathf.RoundToInt( area.height );
			return Screen.height <= height ? 0 : height;
#endif
		}
	}
}