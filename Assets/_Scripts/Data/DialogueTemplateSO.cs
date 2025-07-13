using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New dialogue box", menuName = "Npc/Dialogue Box")]
public class DialogueTemplateSO : ScriptableObject
{
    [SerializeField] private string characterName;
    public string CharacterName { get => characterName; }
    [SerializeField] private Image characterImage;
    public Image CharacterImage { get => characterImage; }
}
