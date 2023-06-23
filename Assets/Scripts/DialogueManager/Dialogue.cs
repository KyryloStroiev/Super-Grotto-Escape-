
using UnityEngine;
[System.Serializable]
public class Dialogue 
{
	public string nameNpc;

	[TextArea(3, 10)]
	public string[] sentences;
}
