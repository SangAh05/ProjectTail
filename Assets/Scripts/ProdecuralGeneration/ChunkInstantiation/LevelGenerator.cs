using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.IO;

public class LevelMetadata{
	public List<RoomInstanceMeta> Rooms;
	public List<Vector3[]> Paths;
	public int RoomCount;

	public LevelMetadata(){
		this.Rooms = new List<RoomInstanceMeta> ();
		this.Paths = new List<Vector3[]> ();
	}
}

public struct DoorMetadata{
	public Vector3 RelativePosition;
	public Vector3 Direction;
	public Vector3 Position;

	public DoorMetadata (Vector3 relativePosition, Vector3 direction, Vector3 position){
		this.RelativePosition = relativePosition;
		this.Direction = direction;
		this.Position = position;
	}	
}

public class RoomInstanceMeta{
	private List<GameObject> connections;
	private List<DoorMetadata> doors;
	private List<TagInstance> tags;
	private Vector3 position;
	private GameObject chunk;
	private Bounds bounds;
	private Rect rect;

	public RoomInstanceMeta(RoomTransformation input){
		this.connections = new List<GameObject> ();
		this.doors = new List<DoorMetadata> ();
		this.chunk = input.Chunk;
		this.position = input.Position;
		this.rect = input.Rect;
		input.Connections.ForEach (c => connections.Add (c.Chunk));
		input.Doors.ForEach (d => doors.Add (new DoorMetadata (d.RelPosition, d.Direction, d.Position)));
		this.bounds = FindBounds ();
		this.tags = FindChunkTags ();
	}

	private Bounds FindBounds(){
		Collider collider = chunk.GetComponent<Collider> ();
		if (collider != null) {
			return collider.bounds;
		}
		return new Bounds();
	}

	private List<TagInstance> FindChunkTags(){
		ChunkTags chunkTags = chunk.GetComponent<ChunkTags> ();
		if (chunkTags != null) {
			return chunkTags.Tags;
		}
		return new List<TagInstance> (0);
	}

	public List<GameObject> Connections {
		get {
			return this.connections;
		}
	}

	public Vector3 Position {
		get {
			return this.position;
		}
	}

	public GameObject Chunk {
		get {
			return this.chunk;
		}
	}

	public Bounds Bounds {
		get {
			return this.bounds;
		}
	}

	public Rect Rect {
		get {
			return this.rect;
		}
	}

	public List<DoorMetadata> Doors {
		get {
			return this.doors;
		}
	}

	public List<TagInstance> Tags {
		get {
			return this.tags;
		}
	}
}

public class LevelGenerator {
	private LevelGeneratorPreset preset;
	private bool setLevelToStatic = true;

	public LevelGenerator(){
		preset = new LevelGeneratorPreset ();
	}

	// presetName으로 지정된 프리셋을 Resources에서 로드하고 역직렬화하여 반환
	public LevelGeneratorPreset LoadPreset(string presetName){
		// LevelGeneratorPreset 타입을 대상으로 하는 XML 직렬화기 생성
		XmlSerializer xmlSerializer = new XmlSerializer (typeof(LevelGeneratorPreset));
		// 인게임에서 프리셋을 찾는 기본 Resources 경로
		string path = GlobalPaths.PresetPathIngame;
		// 파일명(확장자 제외)을 붙여 최종 Resources 경로 구성
		string pathWithFilename = path + presetName;// + ".xml";
		// Resources에서 TextAsset으로 프리셋 XML 텍스트 로드
		TextAsset textAsset = Resources.Load(pathWithFilename) as TextAsset;
		// 텍스트를 StringReader로 감싸 XML 역직렬화 입력으로 사용
		TextReader textReader = new StringReader(textAsset.text);
		
		// (대안) 파일 스트림 사용 예시 코드 - 현재는 Resources 사용으로 주석 처리
		//FileStream fileStream = new FileStream (pathWithFilename, FileMode.Open);            
		// XML을 LevelGeneratorPreset 객체로 역직렬화
		LevelGeneratorPreset loadedPreset = xmlSerializer.Deserialize (textReader) as LevelGeneratorPreset;
		// 프리셋 내 머티리얼 경로를 통해 실제 머티리얼 리소스 로드
		loadedPreset.LoadMaterials ();
		//fileStream.Close ();
		// 역직렬화가 성공했다면 현재 preset 필드를 갱신
		if (loadedPreset != null) {
			preset = loadedPreset;
		}

		// 최종적으로 로드된(또는 유지된) 프리셋 반환
		return preset;
	}

	public LevelMetadata GenerateLevel(int seed){
		Random.InitState (seed);
		LevelGraph levelGraph = new LevelGraph ();
		levelGraph.GenerateGraph (preset.RoomCount, preset.CritPathLength, preset.MaxDoors, preset.Distribution);
		ProceduralLevel level = new ProceduralLevel (levelGraph, preset, true);
		return level.LevelMetadata;
	}

	public LevelMetadata GenerateLevel(string presetName, int seed, bool clear){
		LoadPreset (presetName);
		if (preset == null) {
			Debug.LogError ("Error loading Preset");
			return null;
		}
		if (clear) {
			ClearLevel ();
		}
		return GenerateLevel (seed);
	}

	public LevelMetadata GenerateLevel(string presetName, int seed){
		return GenerateLevel (presetName, seed, setLevelToStatic);
	}

	public LevelMetadata GenerateLevel(int seed, bool clear){
		if (clear) {
			ClearLevel ();
		}
		return GenerateLevel (seed);
	}

	public LevelMetadata GenerateLevel(){
		return GenerateLevel ((int)Random.value * 10000);
	}

	public static void ClearLevel(){
		List<GameObject> instances = GameObject.FindGameObjectsWithTag ("ChunkInstance").ToList();
		foreach (GameObject room in instances) {
			Object.DestroyImmediate (room);
		}
		List<GameObject> hallways = GameObject.FindGameObjectsWithTag ("HallwayTemplate").ToList();
		hallways.ForEach (h => h.SetActive (false));
	}

	public bool SetLevelToStatic {
		get {
			return this.setLevelToStatic;
		}
		set {
			setLevelToStatic = value;
		}
	}
}