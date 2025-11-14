# 방(Chunk)과 길목(Hallway) 프리셋 변경 가이드

## 📋 목차
1. [시스템 구조 이해](#시스템-구조-이해)
2. [방(Chunk) 프리셋 변경하기](#방chunk-프리셋-변경하기)
3. [길목(Hallway) 프리셋 변경하기](#길목hallway-프리셋-변경하기)
4. [프리셋 파일의 역할](#프리셋-파일의-역할)
5. [실전 예제](#실전-예제)
6. [문제 해결](#문제-해결)

---

## 시스템 구조 이해

### 프리셋 시스템의 작동 방식

이 프로젝트의 프리셋 시스템은 **두 가지 레벨**로 구성되어 있습니다:

1. **프리셋 파일 (XML)**: 레벨 생성 파라미터 저장
   - 방 개수, 경로 길이, 제약 조건 등
   - 위치: `Assets/Resources/ProceduralGeneration/Presets/`

2. **Resources 폴더**: 실제 프리팹 에셋 저장
   - 방(Chunk) 프리팹: `Assets/Resources/ProceduralGeneration/Chunks/`
   - 길목(Hallway) 프리팹: `Assets/Resources/ProceduralGeneration/Hallways/`

### 중요한 개념

- **프리셋 파일은 Chunk/Hallway를 직접 저장하지 않습니다**
- 레벨 생성 시 `Resources.LoadAll()`을 사용하여 폴더 내 모든 프리팹을 동적으로 로드합니다
- 따라서 프리팹을 교체하려면 **Resources 폴더의 프리팹을 직접 교체**하면 됩니다

---

## 방(Chunk) 프리셋 변경하기

### 방법 1: 기존 Chunk 교체

1. **Chunk 폴더 위치 확인**
   ```
   Assets/Resources/ProceduralGeneration/Chunks/
   ```

2. **기존 Chunk 프리팹 교체**
   - Unity 에디터에서 `Assets/Resources/ProceduralGeneration/Chunks/` 폴더로 이동
   - 교체하고 싶은 Chunk 프리팹을 찾습니다 (예: `OneDoorCorridor.prefab`)
   - 새 Chunk 프리팹을 같은 이름으로 교체하거나, 기존 프리팹을 수정합니다

3. **레벨 재생성**
   - `Window > Level Generator` 메뉴를 엽니다
   - `Generate Level` 버튼을 클릭하여 새 Chunk가 적용된 레벨을 생성합니다

### 방법 2: 새 Chunk 추가

1. **새 Chunk 프리팹 생성**
   - Unity에서 새 Chunk 프리팹을 만듭니다
   - 필수 컴포넌트:
     - `ChunkTags` 컴포넌트 (태그 설정용)
     - `DoorManager` 컴포넌트 (문 관리용)
     - `AbstractBounds` 컴포넌트 (경계 설정용)

2. **Resources 폴더에 배치**
   ```
   Assets/Resources/ProceduralGeneration/Chunks/YourNewChunk.prefab
   ```

3. **태그 설정**
   - Chunk 프리팹에 `ChunkTags` 컴포넌트를 추가합니다
   - User Generated Tags에 원하는 태그를 추가합니다 (예: "Large", "Boss", "Trap")
   - 이 태그는 Constraint에서 사용됩니다

4. **레벨 생성 확인**
   - 레벨 생성 시 새 Chunk가 자동으로 로드되어 사용 가능한 풀에 포함됩니다

### 방법 3: Chunk 제거

1. **Chunk 폴더에서 프리팹 삭제**
   - Unity에서 `Assets/Resources/ProceduralGeneration/Chunks/` 폴더로 이동
   - 사용하지 않을 Chunk 프리팹을 삭제합니다

2. **주의사항**
   - `NewChunk.prefab`는 기본 템플릿이므로 삭제하지 마세요
   - 삭제된 Chunk는 다음 레벨 생성부터 사용되지 않습니다

---

## 길목(Hallway) 프리셋 변경하기

### 방법 1: 기존 Hallway 교체

1. **Hallway 폴더 위치 확인**
   ```
   Assets/Resources/ProceduralGeneration/Hallways/
   ```

2. **기존 Hallway 프리팹 교체**
   - Unity 에디터에서 `Assets/Resources/ProceduralGeneration/Hallways/` 폴더로 이동
   - 교체하고 싶은 Hallway 프리팹을 찾습니다
   - 새 Hallway 프리팹으로 교체하거나 기존 프리팹을 수정합니다

3. **레벨 재생성**
   - `Window > Level Generator` 메뉴를 엽니다
   - `Generate Level` 버튼을 클릭하여 새 Hallway가 적용된 레벨을 생성합니다

### 방법 2: 새 Hallway 추가

1. **새 Hallway 프리팹 생성**
   - Unity에서 새 Hallway 프리팹을 만듭니다
   - 필수 컴포넌트:
     - `HallwayPrototype` 태그가 있는 GameObject
     - `AbstractBounds` 컴포넌트
     - `ChunkTags` 컴포넌트 (태그 설정용)

2. **Resources 폴더에 배치**
   ```
   Assets/Resources/ProceduralGeneration/Hallways/YourNewHallway.prefab
   ```

3. **태그 설정**
   - Hallway 프리팹에 `ChunkTags` 컴포넌트를 추가합니다
   - User Generated Tags에 원하는 태그를 추가합니다
   - 이 태그는 Hallway Constraint에서 사용됩니다

4. **레벨 생성 확인**
   - 레벨 생성 시 새 Hallway가 자동으로 로드되어 사용 가능한 풀에 포함됩니다

### 방법 3: Hallway 제거

1. **Hallway 폴더에서 프리팹 삭제**
   - Unity에서 `Assets/Resources/ProceduralGeneration/Hallways/` 폴더로 이동
   - 사용하지 않을 Hallway 프리팹을 삭제합니다

2. **주의사항**
   - `HallwayTemplate.prefab`는 기본 템플릿이므로 삭제하지 마세요
   - 삭제된 Hallway는 다음 레벨 생성부터 사용되지 않습니다

---

## 프리셋 파일의 역할

### 프리셋 파일이 저장하는 것

프리셋 파일 (XML)은 다음 정보를 저장합니다:

- ✅ 레벨 구조 파라미터
  - `RoomCount`: 방 개수
  - `CritPathLength`: 메인 경로 길이
  - `MaxDoors`: 최대 문 개수
  - `Distribution`: 분포 값

- ✅ 레벨 속성
  - `RoomDistance`: 방 간 거리
  - `Spacing`: 최소 여백
  - `Seed`: 시드 값
  - `IsSeparateRooms`: 방 분리 여부

- ✅ 길목 머티리얼
  - `HallwayMaterials`: 천장, 바닥, 벽 머티리얼 경로
  - `HallwayTiling`: 텍스처 타일링

- ✅ 제약 조건 (Constraints)
  - 방 타입별 제약
  - 태그 기반 제약
  - Fuzzy 속성 제약

### 프리셋 파일이 저장하지 않는 것

- ❌ Chunk 프리팹 목록
- ❌ Hallway 프리팹 목록
- ❌ 실제 프리팹 에셋

이들은 모두 Resources 폴더에서 동적으로 로드됩니다.

---

## 실전 예제

### 예제 1: 던전 테마로 Chunk 교체하기

1. **던전 테마 Chunk 준비**
   ```
   Assets/Resources/ProceduralGeneration/Chunks/
   ├── DungeonRoom1.prefab
   ├── DungeonRoom2.prefab
   ├── DungeonCorridor.prefab
   └── ...
   ```

2. **기존 Chunk 백업**
   - 기존 Chunk들을 다른 폴더로 이동하거나 이름 변경

3. **새 Chunk 배치**
   - 던전 테마 Chunk들을 `Chunks/` 폴더에 배치

4. **레벨 생성**
   - `Window > Level Generator` 열기
   - `Generate Level` 클릭
   - 새 던전 테마 Chunk로 레벨이 생성됩니다

### 예제 2: 특정 Chunk만 사용하도록 제한하기

1. **사용할 Chunk만 남기기**
   ```
   Assets/Resources/ProceduralGeneration/Chunks/
   ├── BossRoom.prefab      (보스 방만)
   ├── TreasureRoom.prefab  (보물 방만)
   └── NewChunk.prefab      (기본 템플릿)
   ```

2. **나머지 Chunk 임시 이동**
   - 사용하지 않을 Chunk들을 `Chunks/` 폴더 밖으로 이동
   - 예: `Assets/Resources/ProceduralGeneration/Chunks_Backup/`

3. **레벨 생성**
   - 이제 `BossRoom`과 `TreasureRoom`만 사용됩니다

### 예제 3: Hallway 스타일 변경하기

1. **새 Hallway 스타일 준비**
   ```
   Assets/Resources/ProceduralGeneration/Hallways/
   ├── NarrowHallway.prefab
   ├── WideHallway.prefab
   └── HallwayTemplate.prefab (기본 템플릿)
   ```

2. **Hallway 태그 설정**
   - 각 Hallway 프리팹에 `ChunkTags` 컴포넌트 추가
   - `NarrowHallway`: "Narrow" 태그
   - `WideHallway`: "Wide" 태그

3. **Constraint 설정**
   - `Window > Level Generator` 열기
   - `Constraints` 섹션에서 Hallway 제약 추가
   - 특정 태그를 가진 Hallway만 사용하도록 설정 가능

---

## 방 크기가 다른 경우

### ✅ 방 크기가 달라도 괜찮습니다!

시스템은 **다양한 크기의 방을 자동으로 처리**합니다. 각 방의 크기는 `MeshCollider.bounds.size`를 통해 동적으로 계산되며, 크기가 다른 방들도 정상적으로 배치됩니다.

### 자동 처리되는 것들

1. **크기 계산**
   - 각 Chunk의 `MeshCollider`를 통해 실제 크기를 자동 계산
   - `RoomTransformation`에서 크기를 저장하고 Rect 계산에 사용

2. **방 분리 (Separate Rooms)**
   - `IsSeparateRooms` 옵션이 활성화되면 크기가 다른 방들도 자동으로 겹침 방지
   - 각 방의 Rect를 기반으로 충돌 감지 및 분리 처리

3. **문 위치**
   - 문의 위치는 상대 위치(`RelPosition`)를 사용하므로 방 크기와 무관하게 올바르게 배치
   - 문은 항상 `DoorDefinition.GlobalSize` (2.3) 그리드에 정렬

### ⚠️ 주의사항 및 설정

#### 1. MeshCollider 필수

**문제**: Chunk 크기를 계산하려면 `MeshCollider`가 필요합니다.

**해결**:
- 모든 Chunk 프리팹에 `MeshCollider` 컴포넌트가 있는지 확인
- `MeshCollider`가 없으면 크기가 `Vector3.zero`로 계산되어 배치 오류 발생

**확인 방법**:
```csharp
// 코드에서 자동으로 확인됨
private Vector3 ChunkSize(GameObject chunk){
    MeshCollider meshCollider = chunk != null ? chunk.GetComponent<MeshCollider>() as MeshCollider : null;
    return meshCollider != null ? meshCollider.bounds.size : Vector3.zero;
}
```

#### 2. Spacing (최소 여백) 설정

**설정 위치**: `Window > Level Generator` > `Level Properties` > `Minimal margin`

**권장 값**:
- 최소: `DoorDefinition.GlobalSize * 2` (약 4.6)
- 최대: `DoorDefinition.GlobalSize * 4` (약 9.2)
- 기본값: 4.0

**설명**:
- 크기가 다른 방들 사이의 최소 간격을 설정
- 큰 방과 작은 방이 함께 있을 때 충분한 간격 확보
- 값이 너무 작으면 방들이 겹칠 수 있음

**설정 방법**:
1. `Window > Level Generator` 열기
2. `Level Properties` 섹션 확장
3. `Separate Rooms` 체크박스 활성화
4. `Minimal margin` 값을 조절 (4.6 ~ 9.2 권장)

#### 3. Grid Alignment (그리드 정렬)

**자동 처리**: 모든 방은 `DoorDefinition.GlobalSize` (2.3) 단위로 그리드에 자동 정렬됩니다.

**중요**: 
- 문의 위치가 그리드에 정렬되므로 방 크기가 달라도 문 연결이 올바르게 작동
- 방 크기가 `DoorDefinition.GlobalSize`의 배수가 아니어도 자동으로 정렬됨

#### 4. 큰 방과 작은 방이 함께 있을 때

**권장 사항**:
- 크기 차이가 너무 크면 (예: 10배 이상) `Spacing` 값을 더 크게 설정
- 큰 방은 보스 방이나 특별한 방으로 사용하고, Constraint로 제한하는 것을 권장

**예시**:
```
작은 방: 2x2
보통 방: 5x5
큰 방: 10x10

→ Spacing을 6.0 이상으로 설정 권장
```

### 실전 예제: 다양한 크기의 방 사용하기

#### 예제 1: 기본 설정

1. **Chunk 준비**
   ```
   SmallRoom.prefab (2x2)
   MediumRoom.prefab (5x5)
   LargeRoom.prefab (10x10)
   ```

2. **프리셋 설정**
   - `Window > Level Generator` 열기
   - `Level Properties` > `Separate Rooms` 체크
   - `Minimal margin`: 6.0 설정

3. **레벨 생성**
   - `Generate Level` 클릭
   - 크기가 다른 방들이 자동으로 배치됨

#### 예제 2: 큰 방만 특정 위치에 배치

1. **태그 설정**
   - `LargeRoom.prefab`에 `ChunkTags` 컴포넌트 추가
   - User Generated Tags: "Large", "Boss"

2. **Constraint 설정**
   - `Window > Level Generator` > `Constraints` 섹션
   - 새 Constraint 추가:
     - `Target`: `EndRoom`
     - `Type`: `UserDefinedTags`
     - `Containing tag(s)`: "Boss"
   - 이렇게 하면 마지막 방에만 큰 방이 배치됨

### 체크리스트

다양한 크기의 방을 사용할 때 확인할 사항:

- [ ] 모든 Chunk 프리팹에 `MeshCollider` 컴포넌트가 있는가?
- [ ] `Separate Rooms` 옵션이 활성화되어 있는가?
- [ ] `Minimal margin` 값이 적절하게 설정되어 있는가? (4.6 ~ 9.2 권장)
- [ ] 크기 차이가 너무 크지 않은가? (10배 이상 차이나면 Spacing 증가 권장)
- [ ] 문이 올바르게 배치되는가? (문은 항상 그리드에 정렬됨)

### 문제 해결

### Q1: 새 Chunk가 레벨에 나타나지 않아요

**해결 방법:**
1. Chunk가 `Assets/Resources/ProceduralGeneration/Chunks/` 폴더에 있는지 확인
2. Chunk 프리팹에 필수 컴포넌트가 있는지 확인:
   - `ChunkTags` 컴포넌트
   - `DoorManager` 컴포넌트
   - `AbstractBounds` 컴포넌트
3. Chunk의 Tag가 "Chunk"로 설정되어 있는지 확인
4. Unity 에디터에서 `Assets > Reimport All` 실행
5. 레벨을 다시 생성해보세요

### Q2: Hallway가 생성되지 않아요

**해결 방법:**
1. Hallway가 `Assets/Resources/ProceduralGeneration/Hallways/` 폴더에 있는지 확인
2. Hallway 프리팹의 루트 GameObject Tag가 "HallwayTemplate"인지 확인
3. Hallway 프리팹에 `AbstractBounds` 컴포넌트가 있는지 확인
4. `HallwayTemplate.prefab`가 폴더에 있는지 확인 (기본 템플릿)
5. Unity 에디터에서 `Assets > Reimport All` 실행

### Q3: 특정 Chunk만 사용하고 싶어요

**해결 방법:**
1. **방법 1**: 사용하지 않을 Chunk를 `Chunks/` 폴더 밖으로 이동
2. **방법 2**: Constraint를 사용하여 특정 태그를 가진 Chunk만 사용
   - `Window > Level Generator` > `Constraints` 섹션
   - 새 Constraint 추가
   - `Target`: `AllRooms` 또는 특정 방 타입
   - `Type`: `UserDefinedTags`
   - `Containing tag(s)`: 사용할 Chunk의 태그 입력

### Q4: 프리셋을 로드했는데 Chunk가 바뀌지 않아요

**원인:**
- 프리셋 파일에는 Chunk 목록이 저장되지 않습니다
- Chunk는 Resources 폴더에서 동적으로 로드됩니다

**해결 방법:**
- 프리셋을 로드한 후에도 Chunk는 현재 `Chunks/` 폴더에 있는 프리팹들을 사용합니다
- Chunk를 변경하려면 Resources 폴더의 프리팹을 직접 교체해야 합니다

### Q5: 여러 프리셋에서 다른 Chunk 세트를 사용하고 싶어요

**해결 방법:**
현재 시스템은 Resources 폴더의 모든 Chunk를 로드하므로, 프리셋별로 다른 Chunk 세트를 사용하려면:

1. **방법 1**: 프리셋별로 Chunk 폴더를 분리하고 코드 수정
   - `ChunkHelper.cs`의 `BuildMetadata()` 메서드 수정 필요
   - 프리셋에 Chunk 경로 정보 추가 필요

2. **방법 2**: Constraint를 활용하여 특정 태그를 가진 Chunk만 사용
   - 각 프리셋마다 다른 태그를 요구하는 Constraint 설정
   - 예: "Dungeon" 프리셋은 "Dungeon" 태그가 있는 Chunk만 사용

---

## 요약

### 핵심 포인트

1. **프리셋 파일 ≠ 프리팹 에셋**
   - 프리셋 파일: 레벨 생성 파라미터만 저장
   - 프리팹 에셋: Resources 폴더에 저장

2. **Chunk/Hallway 변경 방법**
   - Resources 폴더의 프리팹을 직접 교체/추가/삭제
   - 프리셋 파일 수정 불필요

3. **자동 로드 시스템**
   - 레벨 생성 시 Resources 폴더의 모든 프리팹을 자동 로드
   - `Resources.LoadAll<GameObject>()` 사용

4. **태그 기반 필터링**
   - Constraint를 사용하여 특정 태그를 가진 Chunk/Hallway만 사용 가능

### 빠른 참조

| 항목 | 경로 | 변경 방법 |
|------|------|----------|
| Chunk 프리팹 | `Assets/Resources/ProceduralGeneration/Chunks/` | 프리팹 교체/추가/삭제 |
| Hallway 프리팹 | `Assets/Resources/ProceduralGeneration/Hallways/` | 프리팹 교체/추가/삭제 |
| 프리셋 파일 | `Assets/Resources/ProceduralGeneration/Presets/` | 레벨 파라미터만 저장 |

---

## 추가 리소스

- **코드 참조**:
  - `ChunkHelper.cs`: Chunk 로드 로직
  - `HallwayHelper.cs`: Hallway 로드 로직
  - `GlobalPaths.cs`: 경로 상수 정의

- **관련 문서**:
  - Constraint 시스템 가이드
  - ChunkTags 사용법
  - 프리셋 파일 구조 설명

---

**작성자**: LHcom
**작성일**: 2025.11.13
**버전**: 1.0

