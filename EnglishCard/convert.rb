require 'csv'

words = Hash.new
CSV.read('words.csv').each_with_index do |row, index|
	if index != 0 then
		[1, 4].each do |ind|
			row[ind] = row[ind].scan(/\w+/).first
		end
		words[row[1]] = row[2]
		words[row[4]] = row[5]
	end
end

CSV.open("words_out.csv", "wb") do |csv|
	words.each do |key, value|
		csv << [key, value]
	end
end

File.open("insert_words.sql", "w") do |file|
	words.each do |key, value|
		file.write("INSERT INTO Dictionary VALUES (NULL, #{key}, #{value}, NULL, NULL)\n")
	end
end