using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    #region Memo6
    // internal은 한 프로젝트 안에서만... 기본은 internal
    // UnitTest에서 쓰려고 public 써줌
    #endregion
    public class Dealer //: IRegameSituation
    {
        // 배팅금 관련
        #region Money
        private int _money;

        internal void PutMoney(int bettingMoney)
        {
            _money += bettingMoney;
        }
       
        internal int GetMoney()
        {
            int moneyToReturn = _money;
            _money = 0;
            return moneyToReturn;
        }
        #endregion

        // 카드 분배 하기
        #region DrawCard
        private List<Card> _cards = new List<Card>();

        private int _cardIndex;

        public Card DrawCard() // 여기도 public
        {
            #region Memo3
            // 카드 섞고 위에서부터 한장씩 나눠주기
            //Card card = _cards[_cardIndex];
            //_cardIndex++;
            //return card;
            #endregion

            return _cards[_cardIndex++];
        }
        #endregion

        #region Memo4
        //public int Regame(object obj)
        //{
        //    Dealer dealer = (Dealer)obj;
        //    return this._cards.Regame(dealer.obj);
        //}
        #endregion

        // 0. 딜러가 자동으로 랜덤한 카드 20장 만들기
        #region MakeCardList
        public Dealer()
        {
            #region Memo5
            // 숫자가 야구게임 하드시 상수로 해도 되지만, 섯다게임은
            // 카드의 수가 바뀌지 않으니까 그냥 10, 2 로 써도 된다..?
            // 상수를 만들라는 이유는 숫자만 봣을 때 이게 뭔 숫자인지 모르니까 만드는것...
            // 근데 여기선 뭔지 아니까 굳이 만들진 않는다
            #endregion

            for (int i = 1; i <= 10; i++) // 카드 생성 (순서대로)
            {
                // int number = i + 1; // for (int i = 0; i < 10; i++) 일 때
                for (int j = 0; j < 2; j++)
                {
                    // 처음 반복 돌 때의 1 3 8 이 광
                    bool isGwang = (j == 0) && (i == 1 || i == 3 || i == 8);
                    bool isGusa = (j == 0) && (i == 4 || i == 9);
                    bool isSRE = (j == 0) && (i == 4 || i == 7);
                    Card card = new Card(i, isGwang, isGusa, isSRE); // 카드 광, 구사, 멍텅구리
                    _cards.Add(card);
                }
            }

            // 람다식 // 카드를 섞음 shuffle
            _cards = _cards.OrderBy(x => Guid.NewGuid()).ToList();
        }
        #endregion 
    }
}