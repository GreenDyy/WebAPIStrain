----database first, xong vào tool chạy:
Scaffold-DbContext "Data Source=.;Initial Catalog=IRT;Persist Security Info=True;User ID=sa;Password=123;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Entities
    
Scaffold-DbContext "Data Source=.;Initial Catalog=IRT;Persist Security Info=True;User ID=sa;Password=123;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Entities -f

-- để generate lại thì gõ -f