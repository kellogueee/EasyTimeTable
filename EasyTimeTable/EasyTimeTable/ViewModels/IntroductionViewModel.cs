using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class IntroductionViewModel
    {
        readonly IList<IntroductionModel> source;

        //Itemsource
        public ObservableCollection<IntroductionModel> Introductions { get; private set; }

        public IntroductionViewModel()
        {
            source = new List<IntroductionModel>();
            CreateIntroductions();
        }

        void CreateIntroductions()
        {
            source.Add(new IntroductionModel
            {
                ImageSource = "Intro1.png",
                Information = "일정을 추가하려면 표를 직접 눌러서 추가할 수 있고 메뉴 창을 열어서 추가할 수도 있습니다. "
            });
            source.Add(new IntroductionModel
            {
                ImageSource = "Intro2.png",
                Information = "원하는 색깔을 선택하고 요일, 시간, 분 등을 설정합니다. "
            });
            source.Add(new IntroductionModel
            {
                ImageSource = "Intro4.png",
                Information = "처음 화면에 일정이 나타납니다."
            });
            source.Add(new IntroductionModel
            {
                ImageSource = "Intro6.png",
                Information = "주말 또는 야간 일정을 편리하게 바꿔가며 볼 수 있습니다. "
            });
            Introductions = new ObservableCollection<IntroductionModel>(source);
        }
    }
}
