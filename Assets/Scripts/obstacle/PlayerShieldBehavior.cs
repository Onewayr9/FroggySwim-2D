using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldBehavior : MonoBehaviour {

	[Range (0.1f, 3.0f)]
	public float fadeSpeed;    // How fast alpha value decreases.
	public Color fadeColor;

	private Material m_Material;    // Used to store material reference.
	private Color m_Color;            // Used to store color reference.


	void Start ()
	{
		// Get reference to object's material.
		m_Material = GetComponent <Renderer> ().material;

		// Get material's starting color value.
		m_Color = m_Material.color;

		// Must use "StartCoroutine()" to execute 
		// methods with return type of "IEnumerator".
		StartCoroutine (ColorFade ());

	}

	void Update() 
	{
		if (this.gameObject.activeSelf) {
			StartCoroutine (ColorFade ());
		}
	}
		
		

	// This method fades from original color to "fadeColor"
	IEnumerator ColorFade ()
	{
		while (true) {
			// Lerp start value.
			float change = 0.0f;

			// Loop until lerp value is 1 (fully changed)
			while (change < 1.0f)
			{
				// Reduce change value by fadeSpeed amount.
				change += fadeSpeed * Time.deltaTime;

				m_Material.color = Color.Lerp (m_Color, fadeColor, change);

				yield return null;
			}

			change = 0.0f;

			while (change < 1.0f)
			{
				// Reduce change value by fadeSpeed amount.
				change += fadeSpeed * Time.deltaTime;

				m_Material.color = Color.Lerp (m_Color, m_Color, change);

				yield return null;
			}
		}

	}
		
}
