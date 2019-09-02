using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public abstract class Player : Exception
    {
        #region Memo7
        //public Card Card1 { get; set; }
        //public Card Card2 { get; set; }
        // 이런식으로 생각할 수 있지만....
        // 대부분의 경우에는 List (85퍼)
        // 집합 스택 큐 5퍼 , 나머진 딕셔너리? 라는 강사님 생각
        #endregion

        public Player() { Cards = new List<Card>(); }

        #region Memo8
        // 지금 코드는 전 라운드에서 가진 카드 2장을 버리지 않아서
        // 1라운드에서 이긴 사람이 계속 이기는 문제가 있음
        // 그래서 카드를 clear시키는 메소드가 필요함 => 추가됨
        #endregion

        public List<Card> Cards { get; } // auto property

        // prop 탭탭
        public int Money { get; set; }

        public int OneChance { get; set; } // 기사회생

        public abstract int CalculateScore(); // 점수 계산
        
        public string GetCardText()
        {
            StringBuilder builder = new StringBuilder();
            Console.Write("      Player's Card  : ");
            foreach (var card in Cards)
                builder.Append(card.ToString() + "   ");

            return builder.ToString();
        }
    }
}
