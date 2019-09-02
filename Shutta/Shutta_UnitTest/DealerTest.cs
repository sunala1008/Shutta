using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Shutta;

namespace Shutta_UnitTest // 섯다 게임의 단위 테스트
{
    [TestClass]
    public class DealerTest 
    {
        [TestMethod]
        public void 스무장의_카드에는_광이_3장_들어있어야함() // 여기는 한글로 이름 지어주는걸 권장
        {
            // 딜러는 DrawCard, PutMoney, GetMoney 이외에는 다 private되어야함
            Dealer dealer = new Dealer();

            List<Card> cards = new List<Card>();
            for (int i = 0; i < 20; i++)
            {
                Card card = dealer.DrawCard();
                cards.Add(card);
            }

            int kwangCount = 0;
            //for (int i = 0; i < cards.Count; i++) // cards.Count에는 20이란 숫자가 있을거임
            //{
            //    if (cards[i].IsKwang)
            //        kwangCount++;
            //} // 굳이 i쓰면서 반복 돌릴 필요가없어서... ↓

            // for문 계속 쓰는게 좀...? i값이 필요없는 경우니까 위의 경우는...
            // i값이 필요한 경우에만 for문을 쓰고 다른 경우는 foreach쓰는게 좋다.
            // 기본적으로 foreach 쓰는게 좋대...

            foreach (Card card in cards)
                if (card.IsGwang)
                    kwangCount++;

            Assert.AreEqual(3, kwangCount); // 단정문. 광카운트에는 3이 들어있어야 한다.

            // Console.WriteLine(kwangCount); // 그래서 얘는 있으면 안됨

            // 실행?은 test에 window에 test Explorer
            // 파랑 느낌표는 아직 테스트 안되있다는 뜻
            // 테스트 하려는 탭 눌러서 오른쪽 -> run select test
            // 하고나면 녹색불이 들어옴. 그면 테스트가 잘 되었다는 뜻
            // 만약 빨간불이 들어오면 코드 어디가 문제가 있다는 뜻
            
            // 여기서 파란불이 다 들어오면 문제가 없다는 뜻!
            
            // 영어를 잘한다... ㅎ 단위테스트를 잘 만든다. 엄청 촘촘하게
            // 버그가 생길래야 생길 수 없을 정도로 촘촘하게
        }

        [TestMethod]
        public void 스무장의_카드중_1이_두_장_있어야_함() // 여기는 한글로 이름 지어주는걸 권장
        {
            //Dealer dealer = new Dealer();

            //List<Card> cards = new List<Card>();
            //for (int i = 0; i < 20; i++)
            //{
            //    Card card = dealer.DrawCard();
            //    cards.Add(card);
            //}

            //int countOf1 = 0;
            //foreach (Card card in cards)
            //    if (card.Number == 1)
            //        countOf1++;

            //Assert.AreEqual(2, countOf1);

            Dealer dealer = new Dealer();

            List<Card> cards = new List<Card>();
            for (int i = 0; i < 20; i++)
            {
                Card card = dealer.DrawCard();
                cards.Add(card);
            }

            int countOf1 = 0;
            foreach (Card card in cards)
                if (card.Number == 1)
                    countOf1++;

            Assert.AreEqual(2, countOf1);
        }
    }
}
