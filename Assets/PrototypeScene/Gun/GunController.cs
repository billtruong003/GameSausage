using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefabs; // Viên đạn
    [SerializeField] Transform shootPoint; // Điểm bắn đạn
    [SerializeField] BulletShoot DestroyBullet; // Hàm dùng để hủy viên đạn
    [SerializeField] AudioSource ShootSound; // Tiếng bắn đạn
    [SerializeField] float shootCoolDown; // Thời gian cách đoạn để bắn đạn
    [SerializeField] List<GameObject> bullets; // list này chứa pool object dùng để active
    [SerializeField] int bulletReload;
    public int ArBullet = 30; // Số lượng đạn cần có
    public int totalBullet = 90;
    
    float coolDown; // Là thời gian liên tục giảm theo deltatime nhằm kiểm tra độ trễ bắn đạn
    // Start is called before the first frame update
    
    void Start()
    {
        
    }
    private void Awake()
    {
        // Khởi tạo 30 viên đạn và inactive 30 viên
        bullets = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            GameObject bullet_ = Instantiate(BulletPrefabs);
            bullet_.SetActive(false); // Viên đạn inactive
            bullets.Add(bullet_); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        shoot();
        RotateToMouse();
        if (Input.GetKeyDown(KeyCode.R) && ArBullet < 30) {
            StartCoroutine(WaitAndReloadBullet(2));
        }
    }
    void shoot()
    {
        if (Input.GetMouseButton(1) && coolDown <= 0 && ArBullet > 0)
        {
            ShootSound.Play();
            ArBullet--;
            GameObject Bulletclone = null; // Đặt viên đạn bắn ra là rỗng
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    Bulletclone = bullets[i];
                    break;
                }
            }
            // Nếu trong trường hợp là không tồn tại viên đạn nào trong list cả vì tất cả đã bắn hết rồi thì sẽ khởi tạo mới một viên đạn và
            // Tự động add vào list luôn.
            // Tránh lãng phí thêm tài nguyên cho các lần bắn sau
            // (Thực tế thì ở đây đã setup giới hạn đạn nên sẽ ko bị trường hợp này,
            // nhưng vẫn áp dụng trong trường hợp các game bắn súng phi thuyền.)
            if (Bulletclone == null)
            {
                Bulletclone = Instantiate(BulletPrefabs);
                bullets.Add(Bulletclone);
            }

            Bulletclone.transform.position = shootPoint.position;
            Bulletclone.SetActive(true);

            StartCoroutine(WaitAndReturnBullet(Bulletclone, 3f));
            coolDown = shootCoolDown;
        }
    }
    IEnumerator WaitAndReturnBullet(GameObject bullet, float time)
    {
        // Đợi một khoảng thời gian
        yield return new WaitForSeconds(time);

        // Kiểm tra nếu như viên đạn thuộc danh sách Pooling object
        // Nếu có chạy hàm return bullet để nó trả object về list inactive
        if (bullets.Contains(bullet))
        {
            ReturnBullet(bullet);
        }
    }
    // Hàm trả viên đạn về trạng thái ko kích hoạt
    public void ReturnBullet(GameObject bulletReturn)
    {
        bulletReturn.SetActive(false);
    }
    IEnumerator WaitAndReloadBullet(float time)
    {
        bulletReload = 30 - ArBullet;
        totalBullet -= bulletReload;
        if (bulletReload <= 0)
        {
            bulletReload = -totalBullet;
        }
        // Đợi một khoảng thời gian
        yield return new WaitForSeconds(time);
        ArBullet += bulletReload;
    }
    void RotateToMouse()
    {
        Vector3 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        mousePos.z = (Camera.main.transform.position - transform.position).magnitude;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
    
    
}