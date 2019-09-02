using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Card // 여기도 UnitTest에서 쓰려고 public
    {
        #region Memo2
        //private int _number;

        //public int Number 
        //{
        //    get { return _number; }
        //} // 일반적인 프로퍼티
        #endregion

        // auto proterty
        public int Number { get; } // 자동 프로퍼티 // set이 없음 -> 생성자에서만 변경 가능

        public bool IsGwang { get; private set; } // Card 클래스 내에서만 변경 가능
        // 자동 프로퍼티 : propg 하고 탭 두번
        public bool IsGusa { get; private set; } // Card 클래스 내에서만 변경 가능

        public bool IsSRE { get; private set; } // Secret Royal Emissary 암행어사

        // 이거는 뭐였지...
        public Card(int number, bool isGwang, bool isGusa, bool isSRE) // 생성자
        {
            Number = number;
            IsGwang = isGwang;
            IsGusa = isGusa;
            IsSRE = isSRE;
        }

        public override string ToString()
        {
            if (IsGwang)
                return Number + "광"; // 문자열을 더해서 int를 자동으로 string으로 바꿈
            else if (IsGusa)
                return Number + "K";
            else if (IsSRE)
                return Number + "K";
            // 구사카드 (4K 9K = 멍텅구리 구사 / 나머지는 일반 구사)
            // 암행어사 카드 (4K 7K)  // 편의상 세 카드의 구분자를 K (Key)로 통일함
            else
                return Number.ToString(); // ToString()을 해줘야 int가 문자열로 바뀜
        }
    }
}
