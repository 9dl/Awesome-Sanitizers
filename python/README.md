# Test Results: Input Sanitization

This README contains test results for various input sanitization cases.

**Original Input:**
`' OR 1=1 --`
**Sanitized Input:**
`OR 11`

---
**Original Input:**
`' UNION SELECT null, username, password FROM users --`
**Sanitized Input:**
`UNION SELECT null username password FROM users`

---
**Original Input:**
`admin' --`
**Sanitized Input:**
`admin`

---
**Original Input:**
`1' AND 1=1 --`
**Sanitized Input:**
`1 AND 11`

---
**Original Input:**
`1' OR 'x' = 'x' --`
**Sanitized Input:**
`1 OR x  x`

---
**Original Input:**
`1' HAVING 1=1 --`
**Sanitized Input:**
`1 HAVING 11`

---
**Original Input:**
`<script>alert('XSS')</script>`
**Sanitized Input:**
`scriptalertXSSscript`

---
**Original Input:**
`<img src='x' onerror='alert(1)' />`
**Sanitized Input:**
`img srcx onerroralert1`

---
**Original Input:**
`<script>console.log('XSS')</script>`
**Sanitized Input:**
`scriptconsolelogXSSscript`

---
**Original Input:**
`<iframe src='malicious-site.com'></iframe>`
**Sanitized Input:**
`iframe srcmalicioussitecomiframe`

---
**Original Input:**
`<div onclick='alert(2)'>click me</div>`
**Sanitized Input:**
`div onclickalert2click mediv`

---
**Original Input:**
`<a href='javascript:alert(1)'>link</a>`
**Sanitized Input:**
`a hrefjavascriptalert1linka`

---
**Original Input:**
`<script src='http://evil.com/malicious.js'></script>`
**Sanitized Input:**
`script srchttpevilcommaliciousjsscript`

---
**Original Input:**
`1; DROP TABLE users;`
**Sanitized Input:**
`1 DROP TABLE users`

---
**Original Input:**
`echo 'Hacked' > /tmp/test.txt`
**Sanitized Input:**
`echo Hacked  tmptesttxt`

---
**Original Input:**
`ping -c 1 127.0.0.1`
**Sanitized Input:**
`ping c 1 127001`

---
**Original Input:**
`../../../../etc/passwd`
**Sanitized Input:**
`etcpasswd`

---
**Original Input:**
`../admin/../../etc/passwd`
**Sanitized Input:**
`adminetcpasswd`

---
**Original Input:**
`/etc/passwd`
**Sanitized Input:**
`etcpasswd`

---
**Original Input:**
`http://localhost:8080/admin`
**Sanitized Input:**
`httplocalhost8080admin`

---
**Original Input:**
`http://127.0.0.1/admin`
**Sanitized Input:**
`http127001admin`

---
**Original Input:**
`http://example.com/malicious`
**Sanitized Input:**
`httpexamplecommalicious`

---
**Original Input:**
`<img src='http://victim.com/delete?id=1' />`
**Sanitized Input:**
`img srchttpvictimcomdeleteid1`

---
**Original Input:**
`<form action='http://victim.com/delete' method='POST'><input type='hidden' name='id' value='1'></form>`
**Sanitized Input:**
`form actionhttpvictimcomdelete methodPOSTinput typehidden nameid value1form`

---
