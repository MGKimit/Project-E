# 이 파일에 게임 스크립트를 입력합니다.

# image 문을 사용해 이미지를 정의합니다.
# image eileen happy = "eileen_happy.png"
image sasha = "images/sasha.png"
image sasha = im.Scale("sasha.png", 550, 800)
image lab = "images/lab.jpg"
image lab = im.Scale("lab.jpg", 1920, 1080)
image main = "images/main.png"
image main = im.Scale("main.png", 550, 800)

# 게임에서 사용할 캐릭터를 정의합니다.
define a = Character('[mcname]', color="#c8ffc8")
define s = Character('사샤' , color="#c8ffc8")
define r = Character('라디오' , color="#c8ffc8")

# 여기에서부터 게임이 시작합니다.
label start:
    $ mcname = renpy.input("이름을 입력하세요:", default = "교수")

    $ mcname = mcname.strip()


    r "...오전 7시 정각 시보에 이어, 오늘의 헤드라인을 전해드리겠습니다."

    r "오늘 새벽 3시 경, 판게아 대전 이후 패망한 세레스의 일부 군벌들이 모여 탄생한 테러리스트 조직,
    '네오 세레시스트'가 연방 남부의 작은 소도시, 포드코바를 공격했습니다."

    r "다행히 지역 주민들의 용기있는 저항과 빠른 군경의 대응 등으로 테러리스트들은 전멸한 것으로 밝혀졌습니다."

    r "다음 소식입니다. 최근 들어 연방 북부 해안 지역에서 '바다에서 거대한 검은색 물체를 보았다'라는 증언이 끊이질 않고 있는데다,"

    r "인근 지역 주민들은 그 물체가 컨티넨트에서 넘어온 잠수함이라고 주장하고 있습니다."

    r "이에 대해 연방 해군 소속 알렉산드르 포템킨 대령은 '아직은 확실한 게 아무것도 없다'며 소문을 일축했지만, 곧 '만약 그 소문이
    사실이라면, 컨티넨트는 세레스와 비슷한 결말을 맞게 될 것'이라고 경고했습니다."

    r "이것으로 시보를 마치겠습니다. 로마노프 연방과 당신을 위해."

    scene lab with fade

    show sasha at left with dissolve

    s "교수님, 안녕하세요!"

    show main at right with dissolve

    a "사샤구나, 어서 와."

    s "여기 오늘자 신문이랑...저번주에 학생들 퀴즈 본 거 가채점해왔어요."

    a "어디 볼까..."

    a "하아, 사샤야, 연방의 미래가 어둡다."

    s "너무 어렵게 내긴 하셨어요. 상온 상압에서 저항이 0이 되는 이론적인 전도체의 분자 구조라니..."

    s "참, 편지도 하나 와 있던데요."

    a "편지? 딱히 보낼 사람이 없을텐데?"

    hide lab with Dissolve(1.0)

    hide sasha with Dissolve(1.0)

    hide main with Dissolve(1.0)


    "xx년 xx월 xx일. 로마노프 연방 제1 국립 대학 화학과 교수 [mcname] 귀하."

    "편지와 함께 동봉된 기차표를 지참한 채 해당 기차에 탑승할 것."

    "기차가 출발했을 때 귀하가 탑승해있지 않다면 반역 행위로 간주함."

    "끝."

    show lab with fade

    show sasha at left with dissolve

    show main at right with dissolve

    a "...애들 장난도 아니고, 반역 행위라니..."

    s "단순 장난으로 치부할 순 없을 것 같은데요. 정말로 기차표가 동봉되어있어요."

    s "게다가 이 편지...지금 알아챘는데,"

    hide sasha with Dissolve(0.5)

    hide main with Dissolve(0.5)

    show sasha

    s "국장이 찍혀 있어요."

    hide sasha with Dissolve(0.5)

    show sasha at left with dissolve

    show main at right with dissolve

    a "...정말이잖아. 그렇다는 건 적어도 이 편지에 거짓말이 쓰여있진 않다는 건데..."

    s "...어떻게 하실 건가요, 교수님?"

    a "어떻게, 냐고 물어봐도, 너무 갑작스럽잖아..."

    return
