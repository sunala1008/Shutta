using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class Genealogy
    {
        public string Name { get; set; } // 이름
        public int Score { get; set; } // 점수
    }



    class BasicPlayer : Player
    {
        public override int CalculateScore()
        {
            if (Cards[0].IsGwang && Cards[1].IsGwang)
            {
                if ((Cards[0].Number == 3 && Cards[1].Number == 8) 
                    || (Cards[0].Number == 8 && Cards[1].Number == 3))
                    return 100; // 38광땡 : 무조건 승리
                else
                {
                    if (Cards[0].IsSRE && Cards[1].IsSRE)
                        return 1; // 암행어사
                    else
                        return 90;// 광땡 : 38광땡을 제외한 나머지 패 무조건 승리
                }
            }
            else if (Cards[0].IsGusa && Cards[1].IsGusa)
                return 80;  // 멍텅구리 구사 : 광땡제외 무조건 재경기
            else if (Cards[0].Number == Cards[1].Number) // 일반 땡 : 장땡 ~ 일땡
                    return Cards[0].Number + 60; // 61 ~ 70 땡
            else if ((Cards[0].Number == 3 && Cards[1].Number == 7)
                    || (Cards[0].Number == 7 && Cards[1].Number == 3))
                return 0; // 땡잡이
            else if ((Cards[0].Number == 4 && Cards[1].Number == 9) 
                    || (Cards[0].Number == 9 && Cards[1].Number == 4))
                return 50;  // 구사 : 장땡 제외 무조건 재경기
            else
                return (Cards[0].Number + Cards[1].Number) % 10; // 0 ~ 9 끗
        }
    }
}

/*
    @ 삼팔광땡
        -멍텅구리 (4K + 9K) 재경기 ◎
        < 암행어사 (4K + 7K) 이김. but, 광땡 없으면 1끗 : 1 ◎
    @ 광땡               └─ △
        - 구사 (4 + 9 / 4K + 9 / 4 + 9K) 재경기 ◎
        < 땡잡이 (3 + 7) 이김. but, 땡 없으면 0끗 : 0 ◎
    @ 땡
     = 9 8 7 6 5 4 3 2 1 땡
        
    @ 끗
     = 9 8 7 6 5 4 3 2 1 0 끗 
 */
