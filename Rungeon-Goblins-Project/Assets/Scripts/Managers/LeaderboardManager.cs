using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TMP_InputField _usernameInputField;
        [SerializeField] private GameManager gameManager;

        private void Start()
        {
            //LeaderboardCreator.LoggingEnabled = true;
            LoadEntries();
        }

        public void LoadEntries()
        {
            Leaderboards.RnGRank.GetEntries(entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "___ - 0";
                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryTextObjects[i].text = $"{entries[i].Username} - {entries[i].Score}";
            });
        }
        
        public void UploadEntry()
        {
            Leaderboards.RnGRank.UploadNewEntry(_usernameInputField.text, gameManager.GetDistance(), isSuccessful =>
            {
                if (_usernameInputField.text != "" && _usernameInputField.text != " " && _usernameInputField.text != "   ")
                {
                    if (isSuccessful && _usernameInputField.text != "")
                    LoadEntries();
                }
            });
        }
    }
}