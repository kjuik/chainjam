using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DeathsCounter : MonoBehaviour {
	
	public List<Player> players;
	public List<GUIText> labels;
	public List<GameObject> wholeLabels;
	public List<GameObject> finalPlaces;
	
	Dictionary<Player,int> deaths = new Dictionary<Player, int>();
	
	void Start(){
		foreach(Player p in players){
			deaths.Add(p,0);	
		}
	}
	
	public void addDeath(Player p){
		deaths[p]++;
		labels[players.IndexOf(p)].text = deaths[p].ToString();
	}
	
	public void finalize(){
		
		List<int> sortedDeathNumbers = deaths.Values.Distinct().ToList();
		sortedDeathNumbers.Sort();
		sortedDeathNumbers.Reverse();
		
		int pointsCounter = 1;
		foreach (int deathsNumber in sortedDeathNumbers){
			
			foreach(Player p in players){
				if (deaths[p] == deathsNumber){
					ChainJam.AddPoints(p.playerID,pointsCounter);
					animateLabelToNextPlace(players.IndexOf(p));
				}
			}
			
			pointsCounter++;
			
		}
	}
	
	int nextPlaceNo = 0;
	private void animateLabelToNextPlace(int labelNo){
		iTween.MoveTo(wholeLabels[labelNo].gameObject,iTween.Hash(
				"x",finalPlaces[nextPlaceNo].transform.position.x,
				"y",finalPlaces[nextPlaceNo].transform.position.y, 
				"z",finalPlaces[nextPlaceNo].transform.position.z,
				"time",0.03f,
				"easeType", "easeOutSine"));
		
		nextPlaceNo++;
	}
}
