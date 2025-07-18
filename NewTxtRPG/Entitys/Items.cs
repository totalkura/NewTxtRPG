using NewTxtRPG.Structs;

namespace NewTxtRPG.Entitys
{
    // 아이템 목록 클래스
    internal static class Items
    {
        public static readonly ItemInfo[] ItemList =
        {
            //   id,   타입,         이름,           가격,   설명,                                         공격, 방어, 체력, 마나
            new ItemInfo(1, ItemType.Equipment, "초보자의 검",        100,  "초보자의 검입니다. 없는거보단 도움이 됩니다.", 1, 0, 0, 0),
            new ItemInfo(2, ItemType.Equipment, "가죽 튜닉",          200,  "얇은 가죽으로 만든 튜닉입니다. 초보자에게 제격입니다.", 0, 2, 0, 0),
            new ItemInfo(3, ItemType.Equipment, "무딘 철검",          600,  "무뎌져버린 철검 입니다.", 3, 0, 0, 0),
            new ItemInfo(4, ItemType.Equipment, "수련자 갑옷",        900,  "수련에 도움을 주는 갑옷입니다.", 0, 5, 0, 0),
            new ItemInfo(5, ItemType.Equipment, "전사의 검",         1200,  "전사들이 사용하던 검입니다.", 4, 0, 0, 0),
            new ItemInfo(6, ItemType.Equipment, "전사의 갑옷",       1800,  "전사들이 사용하던 갑옷입니다.", 0, 7, 0, 0),
            new ItemInfo(7, ItemType.Equipment, "청동 도끼",         1500,  "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 0, 0),
            new ItemInfo(8, ItemType.Equipment, "무쇠갑옷",          1000,  "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 0, 0),
            new ItemInfo(9, ItemType.Equipment, "스파르타의 창",     1600,  "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 0, 0),
            new ItemInfo(10, ItemType.Equipment, "스파르타의 갑옷",  3500,  "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 0, 0),
            // 소모품 예시
            new ItemInfo(11, ItemType.Consumables, "치유의 물약",       50,  "체력을 30 회복합니다.", 0, 0, 30, 0),
            new ItemInfo(12, ItemType.Consumables, "마나의 물약",     50,  "마나를 30 회복합니다.", 0, 0, 0, 30),
            new ItemInfo(13, ItemType.Consumables, "슈퍼 엘릭서",    200,  "체력과 마나를 동시에 50 회복하는 만능 물약입니다.", 0, 0, 50, 50),
            // 드랍 전용 아이템
            new ItemInfo(14, ItemType.Dropped, "도토리", 10, "귀여운 다람쥐가 볼에 빵빵하게 넣고 다니는 도토리입니다.", 0, 0, 0, 0),
            new ItemInfo(15, ItemType.Dropped, "토끼고기", 10, "귀여운 토끼의 복실복실한 털을 벗겨내 도축한 생고기입니다.", 0, 0, 0, 0),
            new ItemInfo(16, ItemType.Dropped, "박쥐의 날개", 30, "그닥 안귀여운 박쥐에게서 잘라낸 날개입니다.", 0, 0, 0, 0),
            new ItemInfo(17, ItemType.Dropped, "소고기", 50, "우리 모두가 취직하면 사먹고 싶어하는 투쁠 채끝살입니다.", 0, 0, 0, 0),
            new ItemInfo(18, ItemType.Dropped, "우유", 500, "어떤 소가 '날 아무리 짜봐야 우유 안나와'라고 했다지만, 짜니까 나왔네요?", 0, 0, 0, 0),
            new ItemInfo(19, ItemType.Equipment, "바니걸 코스튬", 5000, "만렙토끼 보스 클리어 보상 아이템입니다. 꽉 껴서 아프지만 입을수는 있습니다.", 0, 22, 0, 0),
            new ItemInfo(20, ItemType.Equipment, "뼈다귀", 10000, "??? 보스 클리어 보상 아이템입니다. 들어서 무기로 쓸수 있습니다.", 45, 0, 0, 0)
        };
    }
}
