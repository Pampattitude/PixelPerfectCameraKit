using UnityEngine;
using System.Collections;
using System.IO;
using System;


public class PixelCameraDemoUI : MonoBehaviour
{
	PixelCamera2D _pixelCam;
	GUIStyle _style = new GUIStyle();
	Texture2D _texture;
	Transform _rotatorTile;
	bool _fullscreen = false;


	void Awake()
	{
		_rotatorTile = GameObject.Find( "RotatorTile" ).transform;

		_pixelCam = FindObjectOfType<PixelCamera2D>();
		_texture = new Texture2D( 1, 1 );
		_texture.SetPixel( 0, 0, Color.yellow );
		_texture.Apply();

		_style.normal.background = _texture;
	}


	void OnGUI()
	{
		GUI.skin.label.fontSize = 20;
		GUI.skin.label.fontStyle = FontStyle.Bold;
		GUI.skin.label.normal.textColor = Color.black;
		GUI.skin.toggle.normal.textColor = Color.black;

		GUILayout.BeginVertical( _style );

		GUI.changed = false;
		_fullscreen = GUILayout.Toggle( _fullscreen, "Fullscreen" );
		if( GUI.changed )
			Screen.SetResolution( Screen.width, Screen.height, _fullscreen );

		if( GUILayout.Button( "480 x 270" ) )
			Screen.SetResolution( 480, 270, _fullscreen );

		if( GUILayout.Button( "960 x 540" ) )
			Screen.SetResolution( 960, 540, _fullscreen );

		if( GUILayout.Button( "1280 x 720" ) )
			Screen.SetResolution( 1280, 720, _fullscreen );

		if( GUILayout.Button( "1440 x 810" ) )
			Screen.SetResolution( 1440, 810, _fullscreen );

		if( GUILayout.Button( "1920 x 1080" ) )
			Screen.SetResolution( 1920, 1080, _fullscreen );

		GUILayout.Space( 20 );

		GUILayout.Label( "Max Height Ratio: " + _pixelCam.maxOffAspectRatio.ToString( "0.00" ) );
		_pixelCam.maxOffAspectRatio = GUILayout.HorizontalSlider( _pixelCam.maxOffAspectRatio, 0.05f, 0.99f );
		GUILayout.Space( 10 );


		GUI.changed = false;
		GUILayout.Label( "OffAspectBehavior" );
		if( GUILayout.Button( "Set None" ) )
			_pixelCam.offAspectBehavior = PixelCamera2D.OffAspectBehavior.None;

		if( GUILayout.Button( "Set DisableCrop" ) )
			_pixelCam.offAspectBehavior = PixelCamera2D.OffAspectBehavior.DisableCrop;

		if( GUILayout.Button( "Set SetNearestPerfectFitResolution" ) )
			_pixelCam.offAspectBehavior = PixelCamera2D.OffAspectBehavior.SetNearestPerfectFitResolution;

		if( GUI.changed )
		{
			_pixelCam.SendMessage( "updateTexture", true );
		}

		GUILayout.Space( 20 );

		if( GUILayout.Button( "Take Screenshot" ) )
			StartCoroutine( takeScreenshot() );


		GUILayout.Space( 20 );

		var screenRatio = (float)Screen.height / _pixelCam.pixelHeight;
		var ratio = Mathf.Ceil( screenRatio ) / screenRatio;

		GUILayout.Label( string.Format( "Current Height Ratio: {0}", ratio.ToString( "0.00" ) ) );
		GUI.Label( new Rect( Screen.width - 140, 0, 130, 40 ), string.Format( "{0} x {1}", Screen.width, Screen.height ) );

		GUILayout.EndVertical();
	}


	void Update()
	{
		var zRot = Mathf.PingPong( Time.time * 50f, 120 ) - 60f;
		_rotatorTile.eulerAngles = new Vector3( 0f, 0f, zRot );
	}


	/// <summary>
	/// rounds value to the nearest number in steps of roundToNearest. Ex: found 127 to nearest 5 resulst in 125
	/// </summary>
	/// <returns>The to nearest.</returns>
	/// <param name="value">Value.</param>
	/// <param name="roundToNearest">Round to nearest.</param>
	public float roundToNearest( float value, float roundToNearest )
	{
		return Mathf.Round( value / roundToNearest ) * roundToNearest;
	}


	IEnumerator takeScreenshot()
	{
		yield return new WaitForEndOfFrame();

		var tex = new Texture2D( Screen.width, Screen.height, TextureFormat.ARGB32, false );
		tex.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
		var name = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Desktop ), string.Format( "screenshot_{0}.png", Screen.width ) );
		File.WriteAllBytes( name, tex.EncodeToPNG() );
	}
}
