require 'csv'

words = Hash.new
CSV.read('words.csv').each_with_index do |row, index|
	words[row[0]] = row[1]
end

CSV.open("words_out.csv", "wb") do |csv|
	words.each do |key, value|
		csv << [key, value]
	end
end

File.open("insert_words.sql", "w") do |file|
	file.write("INSERT INTO word(originWord, translateWord)\n")
	words.each do |key, value|
		 file.write("SELECT \'#{key}\', \'#{value}\'\nUNION ALL\n")
	end
end