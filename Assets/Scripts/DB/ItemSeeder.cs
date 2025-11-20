using System.Collections.Generic;
using SQLite4Unity3d;

public static class ItemSeeder
{
    public static List<Item> GetInitialItems()
    {
        return new List<Item>
        {
            new Item
            {
                item_id = 1,
                name = "아이리스",
                type = "flower",
                price = 500,
                flowers_lang = "내일에 대한 믿음이 다시 당신에게 힘을 주기를 바랍니다."
            },
            new Item
            {
                item_id = 2,
                name = "해바라기",
                type = "flower",
                price = 600,
                flowers_lang = "당신의 마음도 늘 빛을 향해 나아가길 바랍니다."
            },
            new Item
            {
                item_id = 3,
                name = "장미",
                type = "flower",
                price = 500,
                flowers_lang = "사랑과 감사가 당신의 마음에도 아름답게 깃들길 바랍니다."
            },
            new Item
            {
                item_id = 4,
                name = "백합",
                type = "flower",
                price = 700,
                flowers_lang = "순수와 행복이 새로운 시작에 당신에게 힘이 되기를 바랍니다."
            },
            new Item
            {
                item_id = 5,
                name = "튤립",
                type = "flower",
                price = 500,
                flowers_lang = "사랑의 고백과 따뜻한 마음이 당신에게 전해지기를 바랍니다."
            },
            new Item
            {
                item_id = 6,
                name = "헤어1",
                type = "hair",
                price = 800,
                flowers_lang = null
            },
            new Item
            {
                item_id = 7,
                name = "헤어2",
                type = "hair",
                price = 800,
                flowers_lang = null
            },
            new Item
            {
                item_id = 8,
                name = "흰 반팔 티",
                type = "top",
                price = 500,
                flowers_lang = null
            },
            new Item
            {
                item_id = 9,
                name = "검은 반팔 티",
                type = "top",
                price = 500,
                flowers_lang = null
            },
            new Item
            {
                item_id = 10,
                name = "반바지",
                type = "bottom",
                price = 600,
                flowers_lang = null
            },
            new Item
            {
                item_id = 11,
                name = "청바지",
                type = "bottom",
                price = 700,
                flowers_lang = null
            },
            new Item
            {
                item_id = 12,
                name = "평범한 신발",
                type = "shoes",
                price = 200,
                flowers_lang = null
            },
            new Item
            {
                item_id = 13,
                name = "샌들",
                type = "shoes",
                price = 500,
                flowers_lang = null
            }
        };
    }
}
