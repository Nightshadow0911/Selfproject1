using System;
internal class Program
{
    private static Character player;
    private static Equipment Weapon;
    private static Equipment Armor;


    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("나이테", "나이트", 1, 10, 5, 100, 1500);

        // 아이템 정보 세팅
        Weapon = new Equipment("칼", +10, "칼입니다.");
        Armor = new Equipment("갑옷", +20, "갑옷입니다.");
    }

    static void DisplayGameIntro()
    {
        Console.Clear(); //게임 시작시 초기화

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
        }
    }

    static void DisplayMyInfo() //상태창 항목
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("캐릭터가 보유한 아이템을 확인할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine($"[{Weapon.Name} | {Weapon.Stat} | {Weapon.Info}]");
        Console.WriteLine($"[{Armor.Name} | {Armor.Stat} | {Armor.Info}]");
        Console.WriteLine();

        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayEquipmentmanage();
                break;
            case 2:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayEquipmentmanage()
    {
        Console.Clear();

        Console.WriteLine("장착 관리");
        Console.WriteLine("아이템을 장착하거나 탈착할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine($"[무기: {player.Weapon.Name} | {player.Weapon.Stat} | {player.Weapon.Info}]");
        Console.WriteLine($"[방어구: {player.Armor.Name} | {player.Armor.Stat} | {player.Armor.Info}]");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 무기 장착");
        Console.WriteLine("2. 방어구 장착");
        Console.WriteLine("3. 무기 탈착");
        Console.WriteLine("4. 방어구 탈착");

        int input = CheckValidInput(0, 4);
        switch (input)
        {
            case 0:
                DisplayInventory();
                break;
            case 1:
                EquipItem(true);
                break;
            case 2:
                EquipItem(false);
                break;
            case 3:
                UnequipItem(true);
                break;
            case 4:
                UnequipItem(false);
                break;
        }
    }
    static void EquipItem(bool isWeapon)
    {
        Console.Clear();
        Equipment equipment = isWeapon ? Weapon : Armor;
        Equipment currentEquipment = isWeapon ? player.Weapon : player.Armor;

        Console.WriteLine($"현재 {currentEquipment.Name}가 장착되어 있습니다.");
        Console.WriteLine($"새로운 장비로 {equipment.Name}을(를) 장착하시겠습니까? (y/n)");

        string choice = Console.ReadLine();
        if (choice.ToLower() == "y")
        {
            if (isWeapon)
                player.Weapon = equipment;
            else
                player.Armor = equipment;

            Console.WriteLine($"{equipment.Name}을(를) 장착했습니다.");
        }
        else
        {
            Console.WriteLine($"{equipment.Name}을(를) 장착하지 않았습니다.");
        }

        Console.WriteLine("아무 키나 누르세요...");
        Console.ReadKey();
        DisplayEquipmentmanage();
        if (isWeapon)
            player.Weapon = equipment;  // player.weapon 대신 player.Weapon으로 수정
        else
            player.Armor = equipment;   // player.armor 대신 player.Armor로 수정

    }
    static void UnequipItem(bool isWeapon)
    {
        Console.Clear();
        Equipment currentEquipment = isWeapon ? player.Weapon : player.Armor;

        Console.WriteLine($"현재 {currentEquipment.Name}가 장착되어 있습니다.");
        Console.WriteLine($"장착한 {currentEquipment.Name}을(를) 탈착하시겠습니까? (y/n)");

        string choice = Console.ReadLine();
        if (choice.ToLower() == "y")
        {
            if (isWeapon)
                player.Weapon = null;
            else
                player.Armor = null;

            Console.WriteLine($"{currentEquipment.Name}을(를) 탈착했습니다.");
        }
        else
        {
            Console.WriteLine($"{currentEquipment.Name}을(를) 탈착하지 않았습니다.");
        }

        Console.WriteLine("아무 키나 누르세요...");
        Console.ReadKey();
        DisplayEquipmentmanage();
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}


public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }

}
public class Equipment
{
    public string Name { get; }
    public int Stat { get; }
    public string Info { get; }


    public Equipment(string name, int stat, string info)
    {
        Name = name;
        Stat = stat;
        Info = info;
    }

}