### Điểm tốt của thiết kế

1. Có phân chia ra các lớp cụ thể với những vai trò riêng
2. Codeflow khá dễ dàng để theo dõi với entry point là GameManager.cs
3. Code đơn giản, phục vụ tốt cho mục đích prototype nhanh chóng

### Điểm chưa tốt của thiết kế

Trong thiết kế này tồn tại rất nhiều vi phạm về design principle như Single Responsibility (tối thiểu trách nhiệm cho 1 lớp), Inversion of Control (đảo ngược điều khiển từ 1 lớp to thành về các lớp nhỏ hơn), Dependency Inversion (các lớp chỉ nên phụ thuộc vào Interface/Abstract Class), Open-Close Principle (Open để mở rộng và không khuyến khích sửa đổi 1 thành phần đã chạy ổn định). Một vài vấn đề cụ thể có thể kể ra:

1. Bị chồng chéo, lệ thuộc giữa các concrete classes, cải thiện bằng cách:
	+ Sử dụng nhiều interface, abstract class hơn
	+ Sử dụng dependency injection framework, ví dụ như Zenject
2. Cấu trúc của GameManager có thể được cải thiện:
	+ Sử dụng State Pattern thay vì dùng flag "public eStateGame State". Khi đó thì lớp này sẽ bỏ đi khá nhiều trách nhiệm (setup khi bật game, chờ đợi board controller để game over, v.v...), các công việc đó sẽ được thực thi ở các class State nhỏ.
	+ UIMainManager cũng sẽ bỏ được trách nhiệm lắng nghe sự kiện OnGameStateChange để chuyển UI
	+ Các State nhỏ của GameManager sẽ có những dependencies riêng để thực hiện các công việc tương ứng (vd: Gọi UIMainManager bật UI GameOver, gọi BoardController để Pause Game, v.v..)
3. Cấu trúc của BonusItem bị bó buộc vào 4 loại Bonus. Thiết kế này tạo ra nhiều trách nhiệm cho 1 lớp. Thêm vào đó, có thể dev sẽ phải sửa lớp này nếu sau này Game Design muốn thêm Bonus Item. Cải thiện như sau:
	+ Sửa thành 1 class abstract BonusItem, có hàm abstract là ExplodeView()
	+ Tạo thêm các lớp BonusItem kế thừa lớp này, override hàm ExplodeView() để có effect tương ứng.
4. Cấu trúc của NormalItem bị bó buộc vào 7 loại Item. Thêm vào đó việc quyết định prefab cũng đang hoàn toàn do lớp này phụ trách, trong khi bản thân nó đang là 1 lớp logic. Cải thiện như sau:
	+ Loại bỏ enum eNormalType trong code
	+ Cho phép configure 7 loại item này trong ScriptableObject, có thể cân nhắc dùng string Id
	+ Hàm SetType sẽ gán id cho NormalItem
	+ NormalItem có hàm GetPrefabById, lấy prefab từ trong ScriptableObject
	+ Các thành phần khác nên lấy ra loại của NormalItem từ trong ScriptableObject, thay vì sử dụng enum

Nhìn chung, đây là 1 thiết kế phục vụ tốt cho mục đích Prototype nhanh, nhưng có thể gây ra vấn đề sau này
