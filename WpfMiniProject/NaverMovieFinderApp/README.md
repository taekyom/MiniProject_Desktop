## 🎬 Naver Movie Finder<br/>
네이버 오픈 API와 구글의 YouTube Data API를 이용한 영화 검색 프로그램<br/>

#### 🎬 프로그램 작성
1. [NaverDevelopers](https://developers.naver.com/products/intro/plan/plan.md)에서 오픈 API를 받아온다.
2. 검색한 영화 리스트를 WPF의 왼쪽 그리드에 출력한다.
3. 리스트에서 원하는 영화를 선택하면 오른쪽 그리드에 해당 영화의 포스터가 출력된다.<br/><br/>
![검색결과](https://github.com/taekyom/MiniProject_Desktop/blob/main/WpfMiniProject/NaverMovieFinderApp/MOVIEFINDER1.gif "검색결과화면")<br/><br/>
4. 하단의 '예고편 보기' 버튼을 누르면 TrailerWindow가 생성되고 예고편 리스트를 출력한다. <br/>
4-1. 예고편을 선택하면 오른쪽 그리드에 유튜브 공식 예고편이 재생된다.([Youtube API](https://console.cloud.google.com/)를 연동하여 예고편 웹페이지로 연결 가능)<br/><br/>
![유튜브예고편](https://github.com/taekyom/MiniProject_Desktop/blob/main/WpfMiniProject/NaverMovieFinderApp/MOVIEFINDER2.gif "유튜브 예고편화면")<br/><br/>
5. 하단의 '네이버 영화' 버튼을 누르면 네이버 영화 웹페이지로 이동한다. <br/><br/>
![네이버영화](https://github.com/taekyom/MiniProject_Desktop/blob/main/WpfMiniProject/NaverMovieFinderApp/MOVIEFINDER3.gif "네이버영화화면")<br/><br/>
6. 하단의 '즐겨찾기 추가' 버튼을 누르면 연결된 DB에 데이터를 삽입한다.(영화 리스트 중 하나의 셀을 선택한 상태일 때 INSERT 가능)
7. 하단의 '즐겨찾기 보기' 버튼을 누르면 DB에 저장된 즐겨찾기 리스트를 불러와 그리드에 출력한다.<br/>
7-1. '즐겨찾기 보기' 상태에서는 '즐겨찾기 추가' 버튼이 작동하지 않도록 막아준다.<br/>
7-2. '즐겨찾기 보기' 상태에서 하나의 영화를 선택하고 하단의 '즐겨찾기 삭제' 버튼을 누르면 DB의 데이터를 삭제한다.<br/><br/>
![즐겨찾기](https://github.com/taekyom/MiniProject_Desktop/blob/main/WpfMiniProject/NaverMovieFinderApp/MOVIEFINDER4.gif "즐겨찾기 화면")

