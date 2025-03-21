using GoodsAPI.Data;
using GoodsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly GoodDbContext _context;

        public HangHoaController(GoodDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả hàng hóa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoa>>> GetGoods()
        {
            return await _context.Goods.ToListAsync();
        }

        // Lấy hàng hóa theo mã
        [HttpGet("{maHangHoa}")]
        public async Task<ActionResult<HangHoa>> GetGood(string maHangHoa)
        {
            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return hangHoa;
        }

        // Thêm mới hàng hóa
        [HttpPost]
        public async Task<ActionResult<HangHoa>> PostGood(HangHoa hangHoa)
        {
            _context.Goods.Add(hangHoa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGood), new { maHangHoa = hangHoa.MaHangHoa }, hangHoa);
        }

        // Cập nhật hàng hóa
        [HttpPut("{maHangHoa}")]
        public async Task<IActionResult> PutGood(string maHangHoa, HangHoa hangHoa)
        {
            if (maHangHoa != hangHoa.MaHangHoa)
            {
                return BadRequest();
            }

            _context.Entry(hangHoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodExists(maHangHoa))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // Xóa hàng hóa
        [HttpDelete("{maHangHoa}")]
        public async Task<IActionResult> DeleteGood(string maHangHoa)
        {
            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }

            _context.Goods.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{maHangHoa}/ghi-chu")]
        public async Task<IActionResult> UpdateGhiChu(string maHangHoa, [FromBody] string ghiChu)
        {
            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }

            hangHoa.GhiChu = ghiChu;
            await _context.SaveChangesAsync();

            return Ok(hangHoa);
        }

        private bool GoodExists(string maHangHoa)
        {
            return _context.Goods.Any(e => e.MaHangHoa == maHangHoa);


        }
    }
}

