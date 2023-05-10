using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/DialogData", order = 1)]
public class dialogData : ScriptableObject
{
	[TextArea]public List<string> dialogs;
}
