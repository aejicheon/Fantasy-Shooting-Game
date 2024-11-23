using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace OCTOVERSE_EH2LITE_PREVIEW
{
	public class preview : MonoBehaviour
	{
		private string findKey = "EH2";
		private string targetPath = "EH2DSLITE/TRACKS";
		public GameObject messageUI;
		public Text musicTitle;
		private AudioSource audiosource;
		private List<AudioClip> audiofile = new List<AudioClip>();
		private int id = 0;

		void Awake()
		{
			audiosource = GetComponent<AudioSource>();

#if UNITY_EDITOR
			string[] guids2 = AssetDatabase.FindAssets(findKey, new[] { "Assets/" + targetPath });

			foreach (string guid2 in guids2)
			{
				AudioClip ac = (AudioClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid2), typeof(AudioClip));
				audiofile.Add(ac);
			}

			messageUI.SetActive(false);
#endif
		}

		void Start()
		{
			audiosource.PlayOneShot(audiofile[0]);
			//musicTitle.text = audiofile[id].name.Replace("_" + findKey + "_", " - ");
			musicTitle.text = audiofile[id].name;
		}

		public void NextButton()
		{
			if (id >= 0 && id < (audiofile.Count - 1))
			{
				id += 1;
			}
			else if (id == (audiofile.Count - 1))
			{
				id = 0;
			}

			audiosource.Stop();
			StartCoroutine(StartingPlayDelayed(audiosource, audiofile[id]));
		}

		public void PreviousButton()
		{
			if (id > 0)
			{
				id -= 1;
			}
			else if (id == 0)
			{
				id = audiofile.Count - 1;
			}

			audiosource.Stop();
			StartCoroutine(StartingPlayDelayed(audiosource, audiofile[id]));
		}

		private IEnumerator StartingPlayDelayed(AudioSource _audiosource, AudioClip _sfx)
		{
			yield return new WaitForSecondsRealtime(0.5f);
			_audiosource.PlayOneShot(audiofile[id]);
			//musicTitle.text = _sfx.name.Replace("_" + findKey + "_", " - ");
			musicTitle.text = _sfx.name;
		}
	}
}