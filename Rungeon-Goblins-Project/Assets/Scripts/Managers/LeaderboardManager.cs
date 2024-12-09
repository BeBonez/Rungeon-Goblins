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
            //UploadLocal();
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
                    _entryTextObjects[i].text = $"{entries[i].Rank}.{entries[i].Username} - {entries[i].Score}";
            });
        }
        
        private void UploadLocal()
        {
            LeaderboardCreator.UploadNewEntry(Leaderboards.RnGRank.PublicKey, PlayerPrefs.GetString("LocalName", ""), PlayerPrefs.GetInt("PersonalBest", 0), isSuccessful =>
            {
            });
        }
        public void UploadEntry()
        {
            string name = _usernameInputField.text;
            name = name.ToUpper();

            PlayerPrefs.SetString("LocalName", name);
            PlayerPrefs.SetInt("PersonalBest", gameManager.GetDistance());

            LeaderboardCreator.UploadNewEntry(Leaderboards.RnGRank.PublicKey, name, gameManager.GetDistance(), ((msg) =>
            {
                LoadEntries();
            }));
        }
    }
}