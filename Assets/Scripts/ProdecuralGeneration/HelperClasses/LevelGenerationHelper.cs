using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationHelper : MonoBehaviour {

	// 레벨 생성 전에 씬에 함께 배치할 프리팹 목록
	public GameObject[] prefabs;
	// 레벨 생성에 사용할 기본 시드 값 (randomSeed가 false일 때 사용)
	public int seed = 0;
	// true이면 0~9999 범위의 랜덤 시드를 사용, false이면 seed 사용
	public bool randomSeed = false;
	// Resources에 저장된 레벨 프리셋 파일 이름 (확장자 제외)
	public string presetName;

	// Use this for initialization
	void Start () {
		foreach (GameObject go in prefabs) { // go: 인스턴스화할 각 프리팹 원본 참조
			Instantiate (go);
		}
		LevelGenerator levelGenerator = new LevelGenerator (); // 레벨 생성기 인스턴스
		int _seed = randomSeed ? Random.Range (0, 10000) : seed; // 실제 사용될 시드 값
		levelGenerator.GenerateLevel (presetName, _seed);
	}
}
