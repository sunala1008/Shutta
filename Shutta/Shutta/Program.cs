using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class Program
    {
        public const int SeedMoney = 500;    // 종잣돈
        public const int BettingMoney = 100; // 배팅금

        static void Main(string[] args)
        {
            #region 섯다 족보
            IEnumerable<Genealogy> Gen = new Genealogy[]
            {
                new Genealogy {Name = "삼팔광땡", Score = 100},
                new Genealogy {Name = "광땡", Score = 90},
                new Genealogy {Name = "멍텅구리 구사", Score = 80},
                new Genealogy {Name = "일땡", Score = 61},
                new Genealogy {Name = "이땡", Score = 62},
                new Genealogy {Name = "삼땡", Score = 63},
                new Genealogy {Name = "사땡", Score = 64},
                new Genealogy {Name = "오땡", Score = 65},
                new Genealogy {Name = "육땡", Score = 66},
                new Genealogy {Name = "칠땡", Score = 67},
                new Genealogy {Name = "팔땡", Score = 68},
                new Genealogy {Name = "구땡", Score = 69},
                new Genealogy {Name = "장땡", Score = 70},
                new Genealogy {Name = "구사", Score = 50},
                new Genealogy {Name = "알리", Score = 15},
                new Genealogy {Name = "독사", Score = 14},
                new Genealogy {Name = "구삥", Score = 13},
                new Genealogy {Name = "장삥", Score = 12},
                new Genealogy {Name = "장사", Score = 11},
                new Genealogy {Name = "세륙", Score = 10}, // 십끗 => 4, 6 만
                new Genealogy {Name = "갑오", Score = 9},  // 구끗
                new Genealogy {Name = "팔끗", Score = 8},
                new Genealogy {Name = "칠끗", Score = 7},
                new Genealogy {Name = "육끗", Score = 6},
                new Genealogy {Name = "오끗", Score = 5},
                new Genealogy {Name = "사끗", Score = 4},
                new Genealogy {Name = "삼끗", Score = 3},
                new Genealogy {Name = "이끗", Score = 2},
                new Genealogy {Name = "일끗", Score = 1},
                new Genealogy {Name = "망통", Score = 0},
                new Genealogy {Name = "암행어사", Score = 1},
                new Genealogy {Name = "땡잡이", Score = 0} // 3, 7 만
            };
            #endregion


            var spgt = from g in Gen where g.Score == 100 select g; // 삼팔광땡
            var gt = from g in Gen where g.Score == 90 select g; // 광땡
            var mtgr = from g in Gen where g.Score == 80 select g; // 멍텅구리 구사

            foreach (Genealogy gen in spgt){ Console.WriteLine(gen.Name); }
            foreach (Genealogy gen in gt) { Console.WriteLine(gen.Name); }



            Console.WriteLine("┌────────────────┐");
            Console.WriteLine("│\t   룰을 선택하세요.       │");
            Console.WriteLine("│\t      1. Basic\t          │");
            Console.WriteLine("│\t      2. Simple\t          │");
            Console.WriteLine("└────────────────┘");
            Console.Write(" ☞ ");
            int input = int.Parse(Console.ReadLine());
            RuleType ruleType = (RuleType)input;
            Console.WriteLine();

            #region Memo1
            // unit test => 테스트 코드들을 모아두는?? 

            //Player p = new Player();
            //p.GoToShcool(); // 배팅하기. 아래 코드보다 이게 나음
            //p.Money -= 100; // 됨
            //p.Cards = new List<Card>(); // 카드 생성? 이 안됨
            //p.Cards.Add(new Card()); // 카드 추가는 됨
            #endregion

            #region 플레이어 2명
            List<Player> players = new List<Player>();
            for (int i = 0; i < 2; i++)
            {
                if (ruleType == RuleType.Basic)
                    players.Add(new BasicPlayer());
                else
                    players.Add(new SimplePlayer());
            } // 각각의 타입에 따른 객체를 오버라이딩하여 생성
            #endregion

            #region Memo - var
            //// C#에서의 var은 타입을 유추할 수 있을 때 사용함.
            //int i = 1;
            //var i2 = 1;
            //string s = "aa";
            //var s2 = "bb";

            //Dictionary<int, List<List<string>>> list = new Dictionary<int, List<List<string>>>();
            //var list2 = new Dictionary<int, List<List<string>>>();
            #endregion

            #region 플레이어들의 종잣돈 지급
            int ChanceNumber = 1;

            foreach (var player in players)
            {
                player.Money = SeedMoney; // 플레이어들에게 500원씩 지급
                player.OneChance = ChanceNumber;
            }
            #endregion

            int round = 1;         // 초기 라운드
            int rematchNumber = 1; // 배팅금 배율

            #region 게임 시작
            while (true)
            {
                #region 올인시 게임 종료
                if (IsAnyoneAllIn(players)) //한명이 올인하면 게임 종료
                {
                    Console.WriteLine("┌────────────────┐");
                    Console.WriteLine("│\t    ~ 이제 그만 ~\t  │");
                    Console.WriteLine("└────────────────┘");
                    Console.WriteLine();
                    break;
                }
                #endregion

                Console.WriteLine(" ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine($"\t     [Round {round++}]");

                // 딜러는 매 라운드마다 하나씩 만들어짐
                Dealer dealer = new Dealer();

                #region 학교 출석 (배팅)
                foreach (var player in players)
                {
                    player.Money -= (BettingMoney * rematchNumber);

                    if (player.Money < 0)
                    {
                        Console.WriteLine("┌────────────────┐");
                        Console.WriteLine("│  ♨ 배팅금액 부족 경기불가 ♨  │");
                        Console.WriteLine("└────────────────┘");
                        return;
                    }
                    else
                        dealer.PutMoney(BettingMoney * rematchNumber);
                }
                #endregion

                #region 카드배분
                foreach (var player in players)
                {
                    player.Cards.Clear();

                    for (int i = 0; i < 2; i++)
                        player.Cards.Add(dealer.DrawCard());

                    Console.WriteLine(player.GetCardText());
                }
                #endregion

                #region 경기 (일반적 / 재경기)

                JudgeRematch(Rematch(players)); // 멍텅구리 구사, 구사, 무승부

                if (Rematch(players) == 80 || Rematch(players) == 50
                    || Rematch(players) == 40)
                {
                    int money = dealer.GetMoney() / 2;

                    for (int i = 0; i < 2; i++)
                        players[i].Money += money;

                    rematchNumber = rematchNumber * 2; // 재경기 시 배팅금 배율 2배
                    continue;
                }
                else  //일반적 상황
                {
                    // 승자 찾기
                    Player winner = FindWinner(players);

                    // 승자에게 상금 주가
                    winner.Money += dealer.GetMoney();

                    rematchNumber = 1; // 배팅금 배율 초기화

                    // 각 플레이어들의 돈 출력
                    for (int i = 0; i < 2; i++)
                    {
                        Console.Write($"     Player {i + 1}'s Money : ");
                        Console.WriteLine("￦ " + players[i].Money);
                    }

                    Console.WriteLine(" ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine();
                }
                #endregion

                PayBackOrGoToThePolice(players); // 기사회생, 경찰서
            }
            #endregion
        }

        #region 도박은 위험합니다.
        private static void PayBackOrGoToThePolice(List<Player> players)
        {
            for (int i = 0; i < 2; i++)
            {
                if (players[i].OneChance == 0 && players[i].Money >= 200)
                    players[i].Money -= 100;
                else if (players[i].OneChance == 0 && players[i].Money == 0)
                {
                    Console.WriteLine("┌────────────────┐");
                    Console.WriteLine("│   당신은 체포 연행되었습니다.  │");
                    Console.WriteLine("│\t도박의 피해자도 당신\t  │");
                    Console.WriteLine("│\t가해자도 당신 입니다.\t  │");
                    Console.WriteLine("└────────────────┘");
                }
            }
        }
        #endregion

        #region 재경기 상황 메세지
        private static void JudgeRematch(int value) // 재경기 상황 메세지 출력
        {
            if (value == 80)
            {
                
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t    ♨ 멍텅구리 ♨\t  │");
                Console.WriteLine("│    배팅 금액을 돌려받습니다.   │");
                Console.WriteLine("│\t배팅 금액을 2배 올려\t  │");
                Console.WriteLine("│\t 재경기를 시작합니다.\t  │");
                Console.WriteLine("└────────────────┘");
                Console.WriteLine(" ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
            }
            else if (value == 50)
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t     ♨ 구사 ♨\t\t  │");
                Console.WriteLine("│    배팅 금액을 돌려받습니다.   │");
                Console.WriteLine("│\t배팅 금액을 2배 올려\t  │");
                Console.WriteLine("│\t 재경기를 시작합니다.\t  │");
                Console.WriteLine("└────────────────┘");
                Console.WriteLine(" ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
            }
            else if (value == 40)
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t     ♨ Draw ♨\t\t  │");
                Console.WriteLine("└────────────────┘");
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t    ♨ 무승부 ♨\t  │");
                Console.WriteLine("│    배팅 금액을 돌려받습니다.   │");
                Console.WriteLine("│\t배팅 금액을 2배 올려\t  │");
                Console.WriteLine("│\t재경기를 시작합니다.\t  │");
                Console.WriteLine("└────────────────┘");
                Console.WriteLine(" ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
            }
        }
        #endregion

        #region 승자 찾기
        private static Player FindWinner(List<Player> players)
        {
            int score0 = players[0].CalculateScore();
            int score1 = players[1].CalculateScore();
            
            #region 삼팔광땡
            if (score0 == 100 || score1 == 100)
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t￥€＄ 삼팔광땡 ￦￦￦\t  │");
                Console.WriteLine("└────────────────┘");
            }
            #endregion

            #region 암행어사
            if (score0 == 90 && score1 == 1)
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│     ◇ 암행어사 출두요~~ ◇    │");
                Console.WriteLine("└────────────────┘");
                return players[1];
            }
            else if (score0 == 1 && score1 == 90)
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│     ◇ 암행어사 출두요~~ ◇    │");
                Console.WriteLine("└────────────────┘");
                return players[0];
            }
            #endregion

            #region 땡잡이
            if (score0 == 0 && (61 <= score1 && score1 <= 70))
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t   ♨ 땡잡았다! ♨\t  │");
                Console.WriteLine("└────────────────┘");
                return players[0];
            }
            else if ((61 <= score0 && score0 <= 70) && score1 == 0)
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t   ♨ 땡잡았다! ♨\t  │");
                Console.WriteLine("└────────────────┘");
                return players[1];
            }
            #endregion

            #region 일반적인 경우
            if (score0 > score1)
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t♨ Player 1's Win! ♨\t  │");
                Console.WriteLine("└────────────────┘");
                return players[0];
            }
            else
            {
                Console.WriteLine("┌────────────────┐");
                Console.WriteLine("│\t♨ Player 2's Win! ♨\t  │");
                Console.WriteLine("└────────────────┘");
                return players[1];
            }
            #endregion
        }
        #endregion

        #region 올인, 기사회생
        private static bool IsAnyoneAllIn(List<Player> players)
        {
            for (int i = 0; i < 2; i++)
            {
                if (players[i].Money == 0 && players[i].OneChance == 1)
                {
                    Console.WriteLine("┌────────────────┐");
                    Console.WriteLine("│\t   ♧ 기사회생 ♧\t  │");
                    Console.WriteLine("└────────────────┘");
                    Console.WriteLine();
                    ChargeBettingMoney(players[i]);
                    return false;
                }
                else if (players[i].Money == 0)
                    return true;
            }
            return false;
        }

        private static void ChargeBettingMoney(Player player)
        {
            player.OneChance = 0;
            player.Money += 100;
        }
        #endregion

        #region 재경기 상황
        private static int Rematch(List<Player> players)
        {
            int score0 = players[0].CalculateScore();
            int score1 = players[1].CalculateScore();

            if (score0 == score1)
                return 40; // 무승부
            else if (score0 < 90 && score1 < 90)
            {
                if (score0 == 80 || score1 == 80)
                    return 80;

                else if (score0 < 70 && score1 < 70)
                {
                    if (score0 == 50 || score1 == 50)
                        return 50;
                    else
                        return 10;
                }
                else
                    return 10;
            }
            else
                return 10;
        }
        #endregion
    }  
}

// 좁을 수록 복잡도 줄어듦 ???